using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPannel;
    [SerializeField] private GameObject scorePannel;
    [SerializeField] private Button play;
    [SerializeField] private Button pause;
    [SerializeField] private Button exit;
    [SerializeField] private GameObject canon;
    [SerializeField] private Transform pos;
    [SerializeField] private ParticleSystem overEffect;
    public bool isGameOver = false;
    public static GameOver instance;
    private string[] objecttags = { "Ball", "Bullet", "PowerUp", "Coin" };

    private void Awake()
    {
        
        instance = this;
       
    }

    private void Start()
    {
        overEffect.transform.position = pos.transform.position;
    }
    public void OnGameOver()
    {
        isGameOver = true;
        Debug.Log(isGameOver);
        Time.timeScale = 0f;
        gameOverPannel.SetActive(true);
        scorePannel.SetActive(false);
        play.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        canon.SetActive(false);
        var effect = overEffect.GetComponent<ParticleSystem>();
        effect.transform.position = pos.position;
        effect.Play();

        //For Derstroying all game object with tag on game over
        for (int i = 0;i< objecttags.Length; i++)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(objecttags[i]);
            foreach (var x in objects)
            {
                Destroy(x);
            }
        }
        AudioController.instance.GameOverSound();

    }

}
