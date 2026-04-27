using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject levelSelectPanel;

    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    void Start()
    {
        levelSelectPanel.SetActive(false);
        UpdateLevelButtons();
    }

    // ✔ 点击“开始游戏” -> 打开关卡选择
    public void StartGame()
    {
        levelSelectPanel.SetActive(true);
    }

    // ✔ 更新关卡解锁状态
    void UpdateLevelButtons()
    {
        level1Button.interactable = true; // 第一关默认开启

        level2Button.interactable =
            PlayerPrefs.GetInt("LevelUnlocked_2", 0) == 1;

        level3Button.interactable =
            PlayerPrefs.GetInt("LevelUnlocked_3", 0) == 1;
    }
    public void BackToMainMenu()
    {
        levelSelectPanel.SetActive(false);
    }

    // ✔ 关卡选择
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene("Level" + index);
    }

    public void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}