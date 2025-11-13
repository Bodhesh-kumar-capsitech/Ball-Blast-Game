using System;
using System.Collections;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
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
    private bool isDragging = false;
    private Vector3 dragTargetPos;
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
       
    }


    //Canon Movement
    void HandleInput()
    {
        //Drag movement for both mouse click and phone
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 screenPos = Input.mousePosition;
            mousePos.z = 0;

            if (screenPos.y < Screen.height * 0.5f)
            {
                dragTargetPos = new Vector3(
                    mousePos.x,
                    transform.position.y,
                    transform.position.z
                );

                transform.position = Vector3.Lerp(transform.position, dragTargetPos, Time.deltaTime * 15f);

            }
        }

        float horizontal = Input.GetAxis("Horizontal");

        //Movement for pc left&right input
        transform.Translate(Vector2.right * horizontal * moveSpeed * Time.deltaTime);

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
