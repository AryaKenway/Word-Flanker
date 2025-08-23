using System;
using UnityEngine;

[System.Serializable]
public class GridTileData
{
    public int tileType;
    public string letter;
}

[System.Serializable]
public class GridSize
{
    public int x;
    public int y;
}

[System.Serializable]
public class LevelData
{
    public int bugCount;
    public int wordCount;
    public int timeSec;
    public int totalScore;
    public GridSize gridSize;
    public GridTileData[] gridData;
}

[System.Serializable]
public class LevelDataCollection
{
    public LevelData[] data;
}
