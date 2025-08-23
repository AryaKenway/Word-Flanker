using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordSelector : MonoBehaviour
{
    private List<LetterTile> selectedTiles = new List<LetterTile>();
    private bool isDragging = false;

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
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit)) { }
            // that’s for 3D world. Since you’re using UI, we’ll use this instead:

            var results = new List<RaycastResult>();
            PointerEventData ped = new PointerEventData(EventSystem.current) { position = mousePos };
            EventSystem.current.RaycastAll(ped, results);

            foreach (var r in results)
            {
                LetterTile tile = r.gameObject.GetComponent<LetterTile>();
                if (tile != null && !selectedTiles.Contains(tile))
                {
                    // TODO: Check adjacency later
                    tile.Highlight();
                    selectedTiles.Add(tile);
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

            Debug.Log("Word formed: " + word);

            // Check dictionary
            if (word.Length >= 3 && DictionaryLoader.Words.Contains(word.ToLower()))
            {
                Debug.Log("VALID word! +" + word.Length + " points");
            }
            else
            {
                Debug.Log("Invalid word.");
            }

            selectedTiles.Clear();
        }
    }
}
