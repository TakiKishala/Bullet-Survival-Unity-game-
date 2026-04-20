using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Load your main game scene
    public void LoadGame()
    {
        SceneManager.LoadScene("MainGame"); 
    }

    public void About()
    {
        SceneManager.LoadScene("About");
    }

    // Quit the game (optional)
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}