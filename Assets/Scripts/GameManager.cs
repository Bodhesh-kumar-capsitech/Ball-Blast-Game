using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Button playButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private GameObject gameover;
    private GameOver over;
    public static GameManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        over = GameOver.instance;
    }
    public void OnPlay()
    {
        Time.timeScale = 1.0f;
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
        AudioController.instance.ButtonClickSound();
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
        pauseButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        AudioController.instance.ButtonClickSound();
    }

    public void OnExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        AudioController.instance.ButtonClickSound();

    }

    public void OnHome()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRestart()
    {
        over.isGameOver = false;
        Debug.Log(over.isGameOver);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        OnPlay();
        AudioController.instance.ButtonClickSound();
    }    
}