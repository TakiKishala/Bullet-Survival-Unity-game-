using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Object.FindAnyObjectByType<CrosshairFollow>().enabled = false; // Disable the CrosshairFollow script to stop the crosshair from following the mouse

        Cursor.visible = true; // Make the cursor visible again
    }



    public void RestartGame()
        {
            Time.timeScale = 1f;
        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("MainGame");
    }
}