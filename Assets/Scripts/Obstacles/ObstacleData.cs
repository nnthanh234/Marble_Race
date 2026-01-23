using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
    public string MapName;
    public List<ObstacleData> LsObstacleData;
}
[Serializable]
public class ObstacleData
{
    public string ObstacleName;
    public ObstacleTransform Transform;

    [Serializable]
    public class ObstacleTransform
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
    }
}

