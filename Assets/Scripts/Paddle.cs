using System.Collections;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region Singleton
    private static Paddle _Instance;

    public static Paddle Instance => _Instance;
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

    private float deltaX;
    private Rigidbody2D rb;
    private Camera maincamera;
    private float PaddleInitialY;
    private float defaultpaddlewidthinpixels = 3;
    private Transform sr;
    [SerializeField]
    private ContactPoint2D ball;
   // public bool PaddleIsTransforming { get; set; }
    public float extendShrinkDuration = 0.5f;
    public float paddleWidth = 0.4f;
    public float paddleHeight = 0.4f;
    public BoxCollider2D boxcol;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maincamera = FindObjectOfType<Camera>();
        PaddleInitialY = transform.position.y;
        sr = GetComponent<Transform>();
        boxcol = GetComponent<BoxCollider2D>();
        Time.timeScale = 1;

    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);


            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchpos.x - transform.position.x;
                    break;
                case TouchPhase.Moved:
                    rb.MovePosition(new Vector2(touchpos.x - deltaX, 0));
                    break;
                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;
                    break;

            }

        }

        PaddleMovement();
    }

    public void StartWidthAnimation(float NewWidth)
    {
        StopAllCoroutines();
        StartCoroutine(AnimatePaddleWidth(NewWidth));
    }

    public IEnumerator AnimatePaddleWidth(float width)
    {
        //PaddleIsTransforming = true;
        //StartCoroutine(ResetPaddleWidthAfterTime(extendShrinkDuration));
        sr.localScale = new Vector2(width, paddleHeight);
        yield return new WaitForSeconds(extendShrinkDuration);
        sr.localScale=new Vector2(paddleWidth, paddleHeight);
       // StartCoroutine(ResetPaddleWidthAfterTime(extendShrinkDuration));
        
        //boxcol.size = new Vector2(width, paddleHeight);
        /*
        if (width > sr.localScale.x)
        {
            //float currentWidth = sr.localScale.x;
            while (currentWidth < width)
            {
                //currentWidth += Time.deltaTime * 2;
                sr.localScale = new Vector2(currentWidth, paddleHeight);
                boxcol.size = new Vector2(currentWidth, paddleHeight);
                yield return null;
            }
        }
        else
        {
            //float currentWidth = sr.localScale.x;
            while (currentWidth > width)
            {
                //currentWidth -= Time.deltaTime * 2;
                sr.localScale = new Vector2(currentWidth, paddleHeight);
                boxcol.size = new Vector2(currentWidth, paddleHeight);
                yield return null;
            }
        }*/
        //boxcol.size = new Vector2(currentWidth, paddleHeight);
        //PaddleIsTransforming = false;
    }

    private IEnumerator ResetPaddleWidthAfterTime(float extendShrinkDuration)
    {
        StartWidthAnimation(paddleWidth);
        yield return new WaitForSeconds(extendShrinkDuration);
        
    }


    void PaddleMovement()
    {
        float paddleShift = (defaultpaddlewidthinpixels - ((defaultpaddlewidthinpixels / 2) * sr.localScale.x)) / 2;
        float leftclamp = 150-paddleShift;
        float rightclamp =950+paddleShift;
        float MousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftclamp, rightclamp);
        float MousePositionWorldX = maincamera.ScreenToWorldPoint(new Vector3(MousePositionPixels, 0, 0)).x;
        transform.position = new Vector3(MousePositionWorldX, PaddleInitialY, 0);
    }
    
    private void OnCollisionEnter2D(Collision2D paddle)
    {
        if (paddle.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRB = paddle.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = paddle.contacts[0].point;
            Vector3 PaddleCenter = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);

            ballRB.velocity = Vector2.zero;

            float difference = PaddleCenter.x - hitPoint.x;

            if (hitPoint.x < PaddleCenter.x)
            {
                ballRB.AddForce(new Vector2(-Mathf.Abs(difference * 200), BallManager.BallSpeed));

            }
            else
            {
                ballRB.AddForce(new Vector2(Mathf.Abs(difference * 200), BallManager.BallSpeed));
            }

        }

    }

   
}
