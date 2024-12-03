using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip interactionSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip menuSong;
    [SerializeField] private AudioClip lobbySong;
    [SerializeField] private AudioClip victorySong;
    [SerializeField] private AudioClip level1Song;
    [SerializeField] private AudioClip level2Song;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public void stopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
    public void interaction()
    {
        if (audioSource != null && interactionSound != null)
        {
            audioSource.PlayOneShot(interactionSound);
        }
    }

    public void victoryTheme()
    {
        if (audioSource != null && victorySong != null)
        {
            audioSource.PlayOneShot(victorySong);
        }
    }

    public void jumpingSound()
    {
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    public void takeDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    public void collectItemSound()
    {
        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }

    public void menuTheme()
    {
        if (audioSource != null && menuSong != null)
        {
            audioSource.PlayOneShot(menuSong);
        }
    }

    public void lobbyTheme()
    {
        if (audioSource != null && lobbySong != null)
        {
            audioSource.PlayOneShot(lobbySong);
        }
    }

    public void level1Theme()
    {
        if (audioSource != null && level1Song != null)
        {
            audioSource.PlayOneShot(level1Song);
        }
    }

    public void level2Theme()
    {
        if (audioSource != null && level2Song != null)
        {
            audioSource.PlayOneShot(level2Song);
        }
    }
}
