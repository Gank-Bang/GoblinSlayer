using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip mainThemeClip;
    public AudioClip diamondPickupClip;
    public AudioClip playerAttackClip;
    public AudioClip playerDamageClip;
    public AudioClip pigDamageClip;
    public AudioClip enemyDamageClip;
    public AudioClip doorOpenClip;
    public AudioClip footstepsClip;

    private AudioSource audioSource;


    void Start()
        {
            // Lance le thème principal au démarrage du jeu
            PlayMainTheme();
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMainTheme()
    {
        audioSource.clip = mainThemeClip;
        audioSource.Play();
    }

    public void PlayDiamondPickup()
    {
        audioSource.clip = diamondPickupClip;
        audioSource.Play();
    }

    public void PlayPlayerAttack()
    {
        audioSource.clip = playerAttackClip;
        audioSource.Play();
    }

    public void PlayPlayerDamage()
    {
        audioSource.clip = playerDamageClip;
        audioSource.Play();
    }

    public void PlayPigDamage()
    {
        audioSource.clip = pigDamageClip;
        audioSource.Play();
    }

    public void PlayEnemyDamage()
    {
        audioSource.clip = enemyDamageClip;
        audioSource.Play();
    }

    public void PlayDoorOpen()
    {
        audioSource.clip = doorOpenClip;
        audioSource.Play();
    }

    public void PlayFootsteps()
    {
        audioSource.clip = footstepsClip;
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
