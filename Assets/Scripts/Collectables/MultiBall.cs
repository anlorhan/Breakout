using System.Linq;

public class MultiBall : Collectable
{
    public static int balcount=1;

    protected override void ApplyEffect()
    {
        
        BallManager.Instance.SpawnBalls(Paddle.Instance.gameObject.transform.position, 2);//BallManager.Instance.Balls.FirstOrDefault().gameObject.transform.position
        balcount += 2;
        /*
       foreach(Ball ball in BallManager.Instance.Balls.ToList())
        {
                BallManager.Instance.SpawnBalls(ball.gameObject.transform.position, 2);
                balcount += 2;
        }*/
    }
}
