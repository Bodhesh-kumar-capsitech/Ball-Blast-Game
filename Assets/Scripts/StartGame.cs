using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    [SerializeField] private Button beginner;
    [SerializeField] private Button intermediate;
    [SerializeField] private Button taptostart;
    public bool isBeginner = false;
    public bool isIntermediate = false;
    public static StartGame instance;

    private void Awake()
    {
        instance = this;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (isBeginner == true)
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1.0f;
        }
        else
        {
            isBeginner = false;
        }
        print("IS beginner" + isBeginner);

        if (isIntermediate == true)
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1.0f;
        }
        else
        {
            isIntermediate = false;
        }
        print("IS intermediate" + isIntermediate);

    }

    public void GameStart()
    {
        beginner.gameObject.SetActive(true);
        intermediate.gameObject.SetActive(true);
        taptostart.gameObject.SetActive(false);
    }

    public void BeginnerLevel()
    {
        isBeginner = true;
        isIntermediate = false;
    }

    public void IntermediateLevel()
    {
        isIntermediate = true;
        isBeginner = false;
    }
}
