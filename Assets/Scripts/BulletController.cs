using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    private GameObject[] bullets;
     public static BulletController instance;

    void Start()
    {
        instance = this;
    }
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y > Camera.main.orthographicSize + 1f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

    }

    public void DestroyOnGameOver()
    {
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (var bullet in bullets)
        {
            Destroy(bullet);
        }
    }

}
