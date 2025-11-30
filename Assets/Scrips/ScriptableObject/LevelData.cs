using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public float xPosition;   // trục X để player hạ ball
    public float distanceZ;   // khoảng cách Z đến tile tiếp theo
}

[CreateAssetMenu(fileName = "LevelData", menuName = "MusicBallHop/LevelData")]
public class LevelData : ScriptableObject
{
    public List<TileData> tiles;
}
