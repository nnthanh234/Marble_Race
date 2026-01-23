using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Transform))]
public class ObstaclesMapSaver : MonoBehaviour
{
    [SerializeField]
    private string mapName;
    [SerializeField]
    private string fileName;

    private List<MapData> lsNewMap = new List<MapData>();


    [ContextMenu("Save Map")]
    public void SaveMap()
    {
        MapData newMapData = new MapData{  MapName = mapName, LsObstacleData = new List<ObstacleData>()};

        foreach (Transform child in transform)
        {
            CollectRecursive(child, newMapData.LsObstacleData);
        }

        string dir = Path.Combine(Application.dataPath, "SavedMaps");
        Directory.CreateDirectory(dir);
        string filePath = Path.Combine(dir, fileName + ".txt");

        // If a file already exists, read and merge its data with the new data
        if (File.Exists(filePath))
        {
            try
            {
                string existingJson = File.ReadAllText(filePath);
                var lsExistingMap = FromJsonArray<MapData>(existingJson).ToList();

                if (lsExistingMap != null)
                {
                    lsExistingMap.Add(newMapData);
                    lsNewMap = lsExistingMap;
                }
                else
                {
                    // If existing file couldn't be parsed into MapData, keep the newMapData (overwrite)
                    Debug.LogWarning($"Existing map file could not be parsed. Overwriting with new data: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Failed to read/merge existing map file: {ex.Message}. Overwriting with new data.");
            }
        }

        var wrapperToSerialize = new Wrapper<MapData> { Maps = lsNewMap.ToArray() };
        string wrappedJson = JsonUtility.ToJson(wrapperToSerialize, true);
        int idxStart = wrappedJson.IndexOf('[');
        int idxEnd = wrappedJson.LastIndexOf(']');
        string json;
        if (idxStart >= 0 && idxEnd > idxStart)
        {
            json = wrappedJson.Substring(idxStart, idxEnd - idxStart + 1);
        }
        else
        {
            // fallback to the wrapped JSON if unexpected format
            json = wrappedJson;
        }

        File.WriteAllText(filePath, json);

        Debug.Log($"{mapName} saved: {filePath}");
    }

    private void CollectRecursive(Transform t, List<ObstacleData> list)
    {
        var go = t.gameObject;
        var od = new ObstacleData
        {
            ObstacleName = go.name,
            Transform = new ObstacleData.ObstacleTransform
            {
                Position = go.transform.position,
                Rotation = go.transform.eulerAngles,
                Scale = go.transform.localScale
            }
        };

        list.Add(od);

        foreach (Transform child in t)
        {
            CollectRecursive(child, list);
        }
    }
    private T[] FromJsonArray<T>(string jsonArray)
    {
        var wrapped = $"{{\"Maps\":{jsonArray}}}";
        var wrapper = JsonUtility.FromJson<Wrapper<T>>(wrapped);
        return wrapper != null ? wrapper.Maps : Array.Empty<T>();
    }
}