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

    private AudioSource mainThemeSource;
    private AudioSource soundEffectSource;
    private AudioSource footstepsSource;

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

        // Créer un AudioSource pour le thème principal
        mainThemeSource = gameObject.AddComponent<AudioSource>();
        mainThemeSource.clip = mainThemeClip;
        mainThemeSource.loop = true;
        mainThemeSource.volume = 0.3f; 
        mainThemeSource.Play();

        // Créer un AudioSource pour les effets sonores
        soundEffectSource = gameObject.AddComponent<AudioSource>();

        // Créer un AudioSource pour les pas
        footstepsSource = gameObject.AddComponent<AudioSource>();
        footstepsSource.clip = footstepsClip;
        footstepsSource.loop = true;
    }

    // Méthode pour jouer le son de ramassage de diamant
    public void PlayDiamondPickup()
    {
        soundEffectSource.PlayOneShot(diamondPickupClip);
    }

    // Méthode pour jouer le son de l'attaque du personnage
    public void PlayPlayerAttack()
    {
        soundEffectSource.PlayOneShot(playerAttackClip);
    }

    // Méthode pour jouer le son de dégât subi par le personnage
    public void PlayPlayerDamage()
    {
        soundEffectSource.PlayOneShot(playerDamageClip);
    }

    // Méthode pour jouer le son de dégât subi par les cochons
    public void PlayPigDamage()
    {
        soundEffectSource.PlayOneShot(pigDamageClip);
    }

    // Méthode pour jouer le son de dégât subi par un méchant
    public void PlayEnemyDamage()
    {
        soundEffectSource.PlayOneShot(enemyDamageClip);
    }

    // Méthode pour jouer le son d'ouverture de porte
    public void PlayDoorOpen()
    {
        soundEffectSource.PlayOneShot(doorOpenClip);
    }

    // Méthode pour jouer le son de pas
   public void PlayFootstepsLoop()
    {
        footstepsSource.Play();
    }

    // Méthode pour arrêter le son de pas
    public void StopFootsteps()
    {
        footstepsSource.Stop();
    }
}
