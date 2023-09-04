using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
      #region Singleton
    private static GameManager _Instance;

    public static GameManager Instance => _Instance;
    private void Awake()
    {
        if (_Instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            _Instance = this;

        }
    }
    #endregion

    public int lives { get; set; }

    public GameObject GameOverPanel;

    public GameObject WinPanel;
    public GameObject StopButton;

    public int AvailibleLives = 1;
    public List<Collectable> CollectableCollection;

    public bool IsGameStarted { get; set; }

    private void Start()
    {
        Time.timeScale = 0;
        lives = AvailibleLives;
        Ball.OnBallDeath += OnBallDeath;
        Brick.OnBrickDestruction += OnBrickDestruction;
    }

    public void DestroyAllCollactables()
    {
        foreach (var collectables in GameObject.FindGameObjectsWithTag("Collectable"))
        {
            Destroy(collectables);
        }
    }

    private void OnBrickDestruction(Brick obj)
    {
        if (BrickManager.Instance.RemainingBricks.Count <= 0)
        {
            BallManager.Instance.Restart();
            CollectableCollection.Clear();
            IsGameStarted = false;
            BrickManager.Instance.LoadNextLevel();
            DestroyAllCollactables();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DestroyAllCollactables();
    }

    private void OnBallDeath(Ball obj)
    {
        if (BallManager.Instance.Balls.Count <= 0)
        {
            lives--;
            if (lives < 1)
            {
                GameOverPanel.SetActive(true);
                StopButton.SetActive(false);

            }
            else
            {
                BallManager.Instance.Restart();
                IsGameStarted = false;
                BrickManager.Instance.LoadLevel(BrickManager.Instance.currentLevel);
            }
        }
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }

    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
    }
}
