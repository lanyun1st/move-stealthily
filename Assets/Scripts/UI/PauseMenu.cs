using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}