using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject letterTilePrefab;
    private LevelData level;

    void Start()
    {
        
        LevelManager.LoadLevels();
        level = LevelManager.CurrentLevel;
        if (level == null)
        {
            Debug.LogError("Level is null!");
            return;
        }

        Debug.Log("Spawning grid: " + level.gridSize.x + "x" + level.gridSize.y);

        // Spawn grid
        for (int row = 0; row < level.gridSize.y; row++)
        {
            for (int col = 0; col < level.gridSize.x; col++)
            {
                int index = row * level.gridSize.x + col;
                string letter = level.gridData[index].letter;

                GameObject tile = Instantiate(letterTilePrefab, transform);
                tile.name = $"Tile_{row}_{col}";
                tile.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = letter;

                // Assign LetterTile info
                LetterTile lt = tile.GetComponent<LetterTile>();
                if (lt != null)
                {
                    lt.SetLetter(letter, row, col);
                }
                else
                {
                    Debug.LogError("LetterTile script missing on prefab!");
                }

                
                tile.transform.localPosition = new Vector3(col * 100, -row * 100, 0);
            }
        }

        
        DictionaryLoader.LoadWords();
        if (DictionaryLoader.Words.Contains("word"))
            Debug.Log("WORD is in dictionary!");
        else
            Debug.Log("WORD not found!");
    }
}
