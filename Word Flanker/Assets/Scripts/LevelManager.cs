using UnityEngine;

public static class LevelManager
{
    public static int SelectedLevel = 0;
    public static LevelData CurrentLevel;

    public static void LoadLevels()
    {
        TextAsset json = Resources.Load<TextAsset>("levelData");
        if (json == null)
        {
            Debug.LogError("Could not find levelData.txt in Resources!");
            return;
        }
        LevelDataCollection allLevels = JsonUtility.FromJson<LevelDataCollection>(json.text);
        CurrentLevel = allLevels.data[SelectedLevel];
    }
}
