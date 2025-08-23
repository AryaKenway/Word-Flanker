using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayLevel(int levelIndex)
    {
        LevelManager.SelectedLevel = levelIndex;
        SceneManager.LoadScene("Game");
    }
}
