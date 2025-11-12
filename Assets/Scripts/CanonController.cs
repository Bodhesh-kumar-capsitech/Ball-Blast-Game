using System.Collections;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float LeftmoveLimit = -1.84f;
    [SerializeField] private float RightmoveLimit = 1.93f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.001f;
    [SerializeField] private float startTime = 0f;
    [SerializeField] private float delayTime = 0.16f;
    [SerializeField] private GameObject powerUpEffect;
    private Rigidbody2D rb;
    public static CanonController instance;

    private float nextFireTime;
    private int moveDirection = 0;

    public bool doubleDamageActive = false;
    public bool shieldCanonActive = false;
    private float powerUpActiveduration = 6f;
    void Start()
    {
        //call method after every time
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("AutoFire", startTime , delayTime);
    }
    void Update()
    {
        
        HandleInput();
        MoveCanon();
       
    }


    //Canon Movement
    void HandleInput()
    {
        if (Input.GetMouseButton(0)) 
        {
            Vector3 clickPos = Input.mousePosition;

            if (clickPos.y < Screen.height * 0.5f)
            {
                if (clickPos.x < Screen.width / 2)
                    moveDirection = -1;
                else
                    moveDirection = 1;
            }

            //Dont move if not clicked to the screen
            else
            {
                moveDirection = 0;
            }
        }
        else
        {
            moveDirection = 0; 
        }

        float horizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontal) > 0.1f)
            moveDirection = horizontal > 0 ? 1 : -1;
    }

    void MoveCanon()
    {
        transform.Translate(Vector2.right * moveDirection * moveSpeed * Time.deltaTime);


        //maximum left & right canon movement
        float clampedX = Mathf.Clamp(transform.position.x, LeftmoveLimit, RightmoveLimit);
        transform.position = new Vector2(clampedX, transform.position.y);
    }

    //Bullet spawn
    void AutoFire()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        AudioController.instance.BulletFireSound();
    }

    public void ActivatePowerUp()
    {
        if (!doubleDamageActive || !shieldCanonActive)
        {
            powerUpEffect.SetActive(true);
            StartCoroutine(DoubleDamageCoroutine());
        }
       
    }

    private IEnumerator DoubleDamageCoroutine()
    {
        doubleDamageActive = true;
        shieldCanonActive = true;
        Debug.Log("Double Damage Activated!");
        yield return new WaitForSeconds(powerUpActiveduration);
        doubleDamageActive = false;
        shieldCanonActive = false;
        powerUpEffect.SetActive(false);
        Debug.Log("Double Damage Ended!");
        print(doubleDamageActive);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            if(shieldCanonActive == false)
            {
                GameOver.instance.OnGameOver();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }

}
