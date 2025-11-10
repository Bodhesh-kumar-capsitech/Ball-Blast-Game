using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private int HP;
    [SerializeField] private TextMeshPro hpText;
    [SerializeField] private float moveSpeed = 1f;
    private float rotaionSpeed = -0.5f;
    [SerializeField] private float leftmoveLimit = -1.84f;
    [SerializeField] private float rightmoveLimit = 1.93f;
    private int moveDirection = 1;
    [SerializeField] private float coin2Pos;
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private bool isEntering = true;   
    [SerializeField] private float entrySpeed = 2f;    
    [SerializeField] private float targetX;


    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float splitChance = 0.3f; // 30% chance to duplicate
    [SerializeField] private float splitScale = 0.3f;  // smaller size for duplicates
    [SerializeField] private float splitOffset = 0.3f; // distance between the new balls
    private float gravityscale = 0.6f;
    private GameObject[] balls;

    private float minScale = 1.0f;
    public static BallController instance;
    private ScoreManager scoreManager;
    private Rigidbody2D rb;
    
    private void Start()
    {
        instance = this;
        scoreManager = ScoreManager.instance;
        UpdateHPText();
        moveDirection = Random.value > 0.5f ? 1 : -1;
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.gravityScale = gravityscale;

        if (transform.position.x < leftmoveLimit || transform.position.x > rightmoveLimit)
        {
            isEntering = true;
            rb.gravityScale = 0; //Stop falling while entering
            //Move upto a random position after entering into the game scene
            float randomMoveLimit = Random.Range(0.5f, 3f);
            targetX = Mathf.Clamp(transform.position.x > 0 ? rightmoveLimit - randomMoveLimit : leftmoveLimit + randomMoveLimit, leftmoveLimit, rightmoveLimit);
            transform.Rotate(0, 0, rotaionSpeed);
        }
        else
        {
            isEntering = false;
        }
    }

    private void Update()
    {
        if (isEntering && Time.timeScale == 1)
        {
            Vector2 targetPos = new Vector2(targetX, transform.position.y);

            transform.Rotate(0, 0, rotaionSpeed * (-moveDirection));
            transform.position = Vector2.MoveTowards(transform.position, targetPos, entrySpeed * Time.deltaTime);

            //Stop entry when inside view
            if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
            {
                isEntering = false;
                rb.gravityScale = gravityscale; //Enable normal fall
            }

            return; //Skip normal movement while entering
        }

        if(Time.timeScale == 0)
        {
            rotaionSpeed = 0;
        }

        MoveBall();
        transform.Rotate(0,0, rotaionSpeed * (-moveDirection));

    }

    //Move ball with in range
    private void MoveBall()
    {
        //transform.Translate(Vector2.right * moveDirection * moveSpeed * Time.deltaTime);
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
        if (transform.position.x > rightmoveLimit)
        {
            transform.position = new Vector2(rightmoveLimit, transform.position.y);
            moveDirection = -1;
        }
        else if (transform.position.x < leftmoveLimit)
        {
            transform.position = new Vector2(leftmoveLimit, transform.position.y);
            moveDirection = 1;
        }
    }

    //Decrease ball hp after collision
    public void TakeDamage(int damage)
    {
        HP -= damage;
        scoreManager.AddScore(damage);
        UpdateHPText();

        if (HP == 0)
        {
            SplitBall();
        }

        if (HP < 0)
        {
            if (transform.localScale.x < minScale)
            {
                //Destroy directly if small
                AudioController.instance.BallDestroySound();
                Destroy(gameObject);
                SpawnCoin();
            }
        }

        if( HP < 0 )
        {
            Destroy(gameObject);
            SpawnCoin();
        }
    }

    //Update the text
    private void UpdateHPText()
    {
        if (hpText != null)
            hpText.text = HP.ToString();
    }

    void SpawnCoin()
    {
        //Left and right spawn positions
        Vector2 leftPos = new Vector2(transform.position.x - splitOffset - 1f, transform.position.y);
        Vector2 rightPos = new Vector2(transform.position.x + splitOffset, transform.position.y);


        Instantiate(coinPrefab, leftPos, Quaternion.identity);
        Instantiate(coinPrefab, rightPos, Quaternion.identity);
    }

    private void SplitBall()
    {
        if (ballPrefab == null) return;

        //Left and right spawn positions
        Vector2 leftPos = new Vector2(transform.position.x - splitOffset, transform.position.y);
        Vector2 rightPos = new Vector2(transform.position.x + splitOffset, transform.position.y);

        //Create two smaller balls
        GameObject ball1 = Instantiate(ballPrefab, leftPos, Quaternion.identity);
        GameObject ball2 = Instantiate(ballPrefab, rightPos, Quaternion.identity);

        //Scale them down
        ball1.transform.localScale = transform.localScale * splitScale;
        ball2.transform.localScale = transform.localScale * splitScale;

        // Adjust HP for new smaller balls
        BallController b1 = ball1.GetComponent<BallController>();
        BallController b2 = ball2.GetComponent<BallController>();

        if (b1 != null) b1.HP = 0;
        if (b2 != null) b2.HP = 0;

        Destroy(ballPrefab);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //Stop spin
            rb.angularVelocity = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {

            int damage = CanonController.instance.doubleDamageActive ? 2 : 1;
            TakeDamage(damage);
            Destroy(collision.gameObject);
        }
    }

    public void DestroyOnGameOver()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach(var ball in balls)
        {
            Destroy(ball);
        }
    }

}
