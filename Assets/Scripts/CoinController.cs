using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float LeftmoveLimit = -1.84f;
    [SerializeField] private float rightmoveLimit = 1.93f;
    [SerializeField] private float moveSpeed = 2f;
    private GameObject[] coins;
    private float minPos = 5.14f;
    private int moveDirection = 1;
    private ScoreManager scoreManager;
    public static CoinController instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        scoreManager = ScoreManager.instance;
    }


    private void Update()
    {

        if (transform.position.y < -minPos)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < LeftmoveLimit || transform.position.x > rightmoveLimit )
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Canon"))
        {
            Debug.Log("Coin collected!");
            scoreManager.AddScore(1);
            Destroy(gameObject);
            AudioController.instance.CoinCollectSound();
        }
    }

    public void DestroyOnGameOver()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (var coin in coins)
        {
            Destroy(coin);
        }
    }


}
