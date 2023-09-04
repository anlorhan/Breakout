using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    #region Singleton
    private static BrickManager _Instance;

    public static BrickManager Instance => _Instance;
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

    public static event Action OnLevelLoaded;

    public Sprite[] Sprites;
    public List<int[,]> Levels { get; set; }
    public List<Brick> RemainingBricks { get; set; }
    public int InitialBrickCount { get; set; }

    private int MaxRows = 25;
    private int MaxCols = 25;

    public int currentLevel;

    private float initialBrickPositionX = -2.375f;
    private float initialBrickPositionY = 4f;
    private float shiftamount=0.68f;

    private GameObject BricksContainer;
    public Brick brickPrefab;
    public Color[] BrickColors;


    private void Start()
    {
        BricksContainer = new GameObject("BricksContainer");
        Levels = LoadLevels();
        GenerateBricks();
        
    }

    public void LoadNextLevel()
    {
        currentLevel++;

        if (currentLevel >= Levels.Count)
        {
            GameManager.Instance.ShowWinPanel();
        }
        else
        {
            LoadLevel(currentLevel);
        }
    }

    private void GenerateBricks()
    {
        RemainingBricks = new List<Brick>();

        int[,] currentLevelData = Levels[currentLevel];
        float currentSpawnX = initialBrickPositionX;
        float currentSpawnY = initialBrickPositionY;
        float zShift = 0;

        for(int row=0; row < MaxRows; row++)
        {
            for(int col = 0; col < MaxCols; col++)
            {
                int brickType = currentLevelData[row, col];

                if (brickType > 0)
                {
                   Brick newBrick = Instantiate(brickPrefab,new Vector3(currentSpawnX,currentSpawnY,0.0f-zShift),Quaternion.identity)as Brick;
                    newBrick.Init(BricksContainer.transform, Sprites[brickType-1],BrickColors[brickType],brickType);

                    RemainingBricks.Add(newBrick);
                    zShift += 0.0001f;
                }
                currentSpawnX += shiftamount;

                if(col+1 == MaxCols)
                {
                    currentSpawnX = initialBrickPositionX;
                }
            }
            currentSpawnY -=0.27f ;
        }
        InitialBrickCount = RemainingBricks.Count;
        OnLevelLoaded?.Invoke();
        
    }

    private List<int[,]> LoadLevels()
    {
        TextAsset text = Resources.Load("Levels") as TextAsset;
        string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        List<int[,]> Levels = new List<int[,]>();
        int[,] currentlevel = new int[MaxRows, MaxCols];
        int currentRow = 0;

        for(int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];

            if (line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                for(int col = 0; col < bricks.Length; col++)
                {
                    currentlevel[currentRow, col] = int.Parse(bricks[col]);
                }
                currentRow++;
            }
            else
            {
                currentRow = 0;
                Levels.Add(currentlevel);
                currentlevel = new int[MaxRows, MaxCols];
            }
        }
        return Levels;
    }


    public void LoadLevel(int level)
    {
        currentLevel = level;
        ClearRemainingBricks();
        GenerateBricks();
        
    }

    private void ClearRemainingBricks()
    {
        foreach(Brick brick in RemainingBricks.ToList())
        {
            Destroy(brick.gameObject);
        }
    }
}
