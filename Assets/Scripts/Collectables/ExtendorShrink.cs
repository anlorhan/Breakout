public class ExtendorShrink : Collectable
{
    public float NewWidth = 0.7f;

    protected override void ApplyEffect()
    {
        if(Paddle.Instance!=null) //&& !Paddle.Instance.PaddleIsTransforming)
        {
            Paddle.Instance.StartWidthAnimation(NewWidth);
        }
    }

}
