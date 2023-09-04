using System;
using Unity.PlasticSCM.Editor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;
    public Text Target;

    public int Score { get; set; }

    private void Awake()
    {
        Brick.OnBrickDestruction += OnBrickDestruction;
        BrickManager.OnLevelLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded()
    {
        UpdateRemainingBricksText();
        UpdateScoreText(0);
    }

    private void UpdateScoreText(int incremenet)
    {
        Score += incremenet;
        string scoreString = Score.ToString().PadLeft(5,'0');
        ScoreText.text = $"SCORE:{Environment.NewLine}{scoreString}";
    }

    private void OnBrickDestruction(Brick obj)
    {
        UpdateRemainingBricksText();
        UpdateScoreText(10);
    }

    private void UpdateRemainingBricksText()
    {
        Target.text = $"TARGET:{Environment.NewLine}{BrickManager.Instance.RemainingBricks.Count} / {BrickManager.Instance.InitialBrickCount}";
    }

    private void OnDisable()
    {
        Brick.OnBrickDestruction -= OnBrickDestruction;
        BrickManager.OnLevelLoaded -= OnLevelLoaded;
    }
}
