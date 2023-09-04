using UnityEngine;

public class DeathWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            Ball ball = collision.GetComponent<Ball>();
            BallManager.Instance.Balls.Remove(ball);
            ball.Die();
            MultiBall.balcount -= 1;
        }
    }
}
