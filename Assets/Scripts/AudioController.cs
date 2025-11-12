using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundSound;
    [SerializeField] private AudioClip bulletSound;
    [SerializeField] private AudioClip ballDestroySound;
    [SerializeField] private AudioClip buttonClickedSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip coinCollectSound;
    public static AudioController instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       
    }

    public void BackgroundSoundPlay()
    {
        audioSource.loop = true;
        audioSource.PlayOneShot(backgroundSound,3f);
    }

    public void BulletFireSound()
    {
        audioSource.loop = false;
        audioSource.PlayOneShot(bulletSound, 4f);
    }

    public void BallDestroySound()
    {
        audioSource.loop = false;
        audioSource.PlayOneShot(ballDestroySound,6f);
    }

    public void ButtonClickSound()
    {
        audioSource.loop = false;
        audioSource.PlayOneShot(buttonClickedSound, 4f);
    }

    public void GameOverSound()
    {
        audioSource.loop = true;
        audioSource.PlayOneShot(gameOverSound,3f);
    }

    public void CoinCollectSound()
    {
        audioSource.loop = false;
        audioSource.PlayOneShot(coinCollectSound, 2.4f);
    }
}
