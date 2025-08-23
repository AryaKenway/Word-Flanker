using UnityEngine;
using UnityEngine.UI;

public class LetterTile : MonoBehaviour
{
    public string Letter;
    public int row, col;
    public bool isSelected = false;

    private Image image;
    private Color defaultColor;

    void Awake()
    {
        image = GetComponent<Image>();
        if (image != null)
            defaultColor = image.color;
    }

    public void SetLetter(string letter, int r, int c)
    {
        Letter = letter;
        row = r;
        col = c;
    }

    public void Highlight()
    {
        isSelected = true;
        if (image != null)
            image.color = Color.yellow;
    }

    public void ResetHighlight()
    {
        isSelected = false;
        if (image != null)
            image.color = defaultColor;
    }
}
