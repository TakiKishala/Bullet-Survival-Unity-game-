using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pausedSettingPanel;

    private GameState currentState = GameState.Playing;

    public static bool playerInvisible = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                pausedSetting();
            }
            else if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
        }
    }

    public void ShowGameOver()
    {
        currentState = GameState.GameOver;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Object.FindAnyObjectByType<CrosshairFollow>().gameObject.SetActive(false);

        Cursor.visible = true;
    }

    public void pausedSetting()
    {
        if (currentState != GameState.Playing)
        {
            return;
        }
        currentState = GameState.Paused;
        pausedSettingPanel.SetActive(true);
        Time.timeScale = 0f;
        Object.FindAnyObjectByType<CrosshairFollow>().enabled = false;

        Cursor.visible = true;

        FindAnyObjectByType<Shooter>().enabled = false;
    }

    public void ResumeGame()
    {
        if (currentState != GameState.Paused)
        {
            return;
        }
        currentState = GameState.Playing;

        pausedSettingPanel.SetActive(false);
        Time.timeScale = 1f;
        Object.FindAnyObjectByType<CrosshairFollow>().enabled = true;
        Cursor.visible = false;

        FindAnyObjectByType<Shooter>().enabled = true;
    }

    public void BeginDying()
    {
        currentState = GameState.Dying;
        Time.timeScale = 0.3f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainGame");
    }
}
