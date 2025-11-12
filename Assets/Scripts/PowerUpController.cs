using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private float minPos = 5.14f;
    public bool isPowerUpActivated;
    public static PowerUpController instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -minPos)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Canon"))
        {
            isPowerUpActivated = true;
            CanonController.instance.ActivatePowerUp();
            Debug.Log("Powerup Activated");
            Destroy(gameObject);
            AudioController.instance.CoinCollectSound();
        }
        
    }
}
