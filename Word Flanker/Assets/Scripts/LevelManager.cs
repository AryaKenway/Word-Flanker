using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    public static int currentLevelIndex = 0;
    public static LevelData[] allLevels;

    public static LevelData CurrentLevel
    {
        get
        {
            if (allLevels == null || allLevels.Length == 0)
                LoadLevels();

            return allLevels[currentLevelIndex];
        }
    }

    public static void LoadLevels()
    {
        allLevels = new LevelData[2];

      
        
        allLevels[0] = new LevelData
        {
            wordCount = 6,
            timeSec = 90,
            totalScore = 0,
            gridSize = new GridSize { x = 4, y = 4 },
            gridData = CreateGrid(new string[,]
            {
                { "C", "A", "B", "S" }, 
                { "D", "O", "I", "U" }, 
                { "G", "N", "R", "N" }, 
                { "M", "O", "T", "E" }  
            })
        };

        
        allLevels[1] = new LevelData
        {
            wordCount = 6,
            timeSec = 30,
            totalScore = 0,
            gridSize = new GridSize { x = 4, y = 4 },
            gridData = CreateGrid(new string[,]
            {
                { "S", "T", "A", "R" }, 
                { "L", "A", "K", "E" }, 
                { "I", "O", "N", "C" }, 
                { "R", "M", "E", "N" }  
            })
        };
    }

    private static GridTileData[] CreateGrid(string[,] letters)
    {
        int rows = letters.GetLength(0);
        int cols = letters.GetLength(1);

        GridTileData[] grid = new GridTileData[rows * cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                grid[r * cols + c] = new GridTileData
                {
                    letter = letters[r, c],
                    tileType = 0
                };
            }
        }

        return grid;
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene("Game");
    }

    public static void NextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex >= allLevels.Length)
        {
            currentLevelIndex = 0;
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }
}
