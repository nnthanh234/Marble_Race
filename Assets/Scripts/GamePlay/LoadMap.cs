using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class LoadMap : Singleton<LoadMap>
{
    [SerializeField]
    private string url = "https://drive.google.com/uc?export=download&id=1wyTq7IeLAyoUDs52JHftW5KI8V03V48r";
    [SerializeField]
    private GameObject objLoadmap;
    [SerializeField]
    private Transform parentObstacles;

    private string prefabEditorFolder = "Assets/Prefabs/Obstacles";
    public MapData[] MapsData { get; private set; }


    private void Start()
    {
        StartDownload();
    }
    public void StartLoad(string mapName)
    {
        MapData mapData = GetMapData(mapName);
        if (mapData == null)
        {
            Debug.LogError("Map Data is null");
            return;
        }

        DestroyChidren();

        foreach (var od in mapData.LsObstacleData)
        {
            GameObject go = null;

#if UNITY_EDITOR
            if (!string.IsNullOrEmpty(od.ObstacleName))
            {
                var prefabPath = $"{prefabEditorFolder}/{od.ObstacleName}.prefab";
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                if (prefab != null)
                {
                    var instantiated = PrefabUtility.InstantiatePrefab(prefab, transform);
                    go = instantiated as GameObject;
                    if (go != null)
                    {
                        go.transform.parent = parentObstacles;
                        go.transform.position = od.Transform != null ? od.Transform.Position : Vector3.zero;
                        go.transform.rotation = od.Transform != null ? Quaternion.Euler(od.Transform.Rotation) : Quaternion.identity;
                        go.transform.localScale = od.Transform != null ? od.Transform.Scale : Vector3.one;
                        go.name = od.ObstacleName;
                    }
                }
            }
#endif
            if (go == null)
            {
                go = new GameObject(od.ObstacleName ?? "Obstacle");
                go.transform.parent = transform;
                go.transform.position = od.Transform != null ? od.Transform.Position : Vector3.zero;
                go.transform.rotation = od.Transform != null ? Quaternion.Euler(od.Transform.Rotation) : Quaternion.identity;
                go.transform.localScale = od.Transform != null ? od.Transform.Scale : Vector3.one;
            }
        }

        Debug.Log($"Map loaded successed!");
    }
    private MapData GetMapData(string mapName)
    {
        for (int i = 0; i < MapsData.Length; i++)
        {
            if (MapsData[i].MapName.Equals(mapName))
            {
                return MapsData[i];
            }
        }

        return null;
    }
    private void DestroyChidren()
    {
        for (int i = parentObstacles.childCount - 1; i >= 0; i--)
        {
            Destroy(parentObstacles.GetChild(i).gameObject);
        }
    }
    private void StartDownload()
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            Debug.LogError("CloudMapDownloader: Url is empty.");
            return;
        }

        StartCoroutine(DownloadAndSaveMap(url));
    }
    private T[] FromJsonArray<T>(string jsonArray)
    {
        var wrapped = $"{{\"Maps\":{jsonArray}}}";
        var wrapper = JsonUtility.FromJson<Wrapper<T>>(wrapped);
        return wrapper != null ? wrapper.Maps : Array.Empty<T>();
    }
    private System.Collections.IEnumerator DownloadAndSaveMap(string url)
    {
        using (var req = UnityWebRequest.Get(url))
        {
            req.timeout = 30;
            yield return req.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
#else
            if (req.isNetworkError || req.isHttpError)
#endif
            {
                Debug.LogError($"CloudMapDownloader: failed to download '{url}': {req.error}");
                yield break;
            }

            string content = req.downloadHandler.text;

            string trimmed = content.TrimStart();
            if (trimmed.StartsWith("{") || trimmed.StartsWith("["))
            {
                try
                {
                    MapsData = FromJsonArray<MapData>(content);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"CloudMapDownloader: JsonUtility.FromJson threw: {ex.Message}");
                }

                if (MapsData != null)
                {
                    DelayBeforeStartGame();
                    yield break;
                }

                Debug.LogWarning("CloudMapDownloader: downloaded JSON doesn't match MapData structure.");
                yield break;
            }

            Debug.LogWarning("CloudMapDownloader: downloaded file is not JSON. Saved raw text. If you want automatic conversion, provide a sample of the txt format so I can add a parser.");
        }
    }
    private async void DelayBeforeStartGame()
    {
        await Task.Delay(1000);
        objLoadmap.SetActive(false);

        GameManager.Instance.StartNewMap();
    }
}
[Serializable]
public class Wrapper<T>
{
    public T[] Maps;
}
