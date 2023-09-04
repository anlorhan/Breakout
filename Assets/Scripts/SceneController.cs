using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject levelselectpanel;
    public GameObject stoppanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }

    public void StopGame()
    {
        stoppanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        stoppanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void MENU()
    {
        SceneManager.LoadScene("MENU");
    }

    public void Level1()
    {
        BrickManager.Instance.LoadLevel(0);
        levelselectpanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void Level2()
    {
        BrickManager.Instance.LoadLevel(1);
        levelselectpanel.SetActive(false);
        Time.timeScale = 1;
    }
}
