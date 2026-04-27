using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelUI : MonoBehaviour
{
    public void NextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;

        // ✔ 保存解锁
        PlayerPrefs.SetInt("LevelUnlocked_" + nextLevel, 1);
        PlayerPrefs.Save();

        // ✔ 进入下一关
        SceneManager.LoadScene(nextLevel);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}