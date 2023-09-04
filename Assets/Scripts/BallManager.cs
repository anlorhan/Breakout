using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    #region Singleton
    private static BallManager _Instance;

    public static BallManager Instance => _Instance;
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

    [SerializeField]
    private Ball BallPrefab;
    private Ball InitialBall;
    public static float BallSpeed = 300;
    private Rigidbody2D InitialBallRB;
    public List<Ball> Balls { get; set; }

    private void Start()
    {
        InitBall();
    }

    private void InitBall()
    {
        Vector3 PaddlePosition = Paddle.Instance.transform.position;
        Vector3 startingPosition = new Vector3(PaddlePosition.x, PaddlePosition.y+.23f,0);
        InitialBall = Instantiate(BallPrefab,startingPosition,Quaternion.identity);
        InitialBallRB = InitialBall.GetComponent<Rigidbody2D>();

        Balls = new List<Ball>
        {
            InitialBall
        };
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            Vector3 PaddlePosition = Paddle.Instance.transform.position;
            Vector3 BallPosition = new Vector3(PaddlePosition.x, PaddlePosition.y + .23f, 0);
            InitialBall.transform.position = BallPosition;

            if (Input.GetMouseButtonDown(0))
            {
                InitialBallRB.isKinematic=false;
                InitialBallRB.AddForce(new Vector2(0, BallSpeed));
                GameManager.Instance.IsGameStarted = true;
            }


        }
    }

    public void SpawnBalls(Vector3 position,int count)
    {
        for(int i = 0; i < count; i++)
        {
            Ball spawnedBall = Instantiate(BallPrefab, position, Quaternion.identity) as Ball;

            Rigidbody2D spawnedBallRb = spawnedBall.GetComponent<Rigidbody2D>();
            spawnedBallRb.isKinematic=false;
            spawnedBallRb.AddForce(new Vector2(0, BallSpeed));
            Balls.Add(spawnedBall);
        }
    }

    public void Restart()
    {
        foreach(var ball in Balls.ToList())
        {
            Destroy(ball.gameObject);
        }
        InitBall();
    }
}
