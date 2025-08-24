using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class WordSelector : MonoBehaviour
{
    private List<LetterTile> selectedTiles = new List<LetterTile>();
    private bool isDragging = false;

    private HashSet<string> usedWords = new HashSet<string>();

    [SerializeField] private TextMeshProUGUI foundWordsText; 
    private List<string> wordListDisplay = new List<string>(); 

    void Start()
    {
        if (foundWordsText != null)
            foundWordsText.text = "Found Words:\n";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            selectedTiles.Clear();
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            PointerEventData ped = new PointerEventData(EventSystem.current) { position = mousePos };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, results);

            foreach (var r in results)
            {
                LetterTile tile = r.gameObject.GetComponent<LetterTile>();
                if (tile == null)
                    tile = r.gameObject.GetComponentInParent<LetterTile>();

                if (tile != null && !selectedTiles.Contains(tile))
                {
                    if (selectedTiles.Count == 0 || IsAdjacent(selectedTiles[^1], tile))
                    {
                        tile.Highlight();
                        selectedTiles.Add(tile);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            string word = "";

            foreach (var tile in selectedTiles)
            {
                word += tile.Letter;
                tile.ResetHighlight();
            }

            if (word.Length >= 3)
            {
                if (DictionaryLoader.Words.Contains(word.ToLower()))
                {
                    if (usedWords.Contains(word.ToLower()))
                    {
                        ShowMessage("Word already used: " + word);
                    }
                    else
                    {
                        usedWords.Add(word.ToLower());
                        wordListDisplay.Add(word.ToUpper());
                        UpdateWordListUI();
                        ShowMessage("VALID word: " + word);
                        GameManager gm = FindFirstObjectByType<GameManager>();
                        if (gm != null)
                            gm.OnWordFound(word);
                    }
                }
                else
                {
                    ShowMessage("Invalid word: " + word);
                }
            }
            else if (word.Length > 0)
            {
                ShowMessage("Too short: " + word);
            }

            selectedTiles.Clear();
        }
    }

    private bool IsAdjacent(LetterTile a, LetterTile b)
    {
        int dx = Mathf.Abs(a.col - b.col);
        int dy = Mathf.Abs(a.row - b.row);
        return dx <= 1 && dy <= 1;
    }

    private void UpdateWordListUI()
    {
        if (foundWordsText != null)
        {
            foundWordsText.text = "Found Words:\n";
            foreach (string w in wordListDisplay)
                foundWordsText.text += w + "\n";
        }
    }

    private void ShowMessage(string msg)
    {
        Debug.Log(msg); 
        
    }
}
