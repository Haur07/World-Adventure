using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip interactionSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip menuSong;
    [SerializeField] private AudioClip lobbySong;
    [SerializeField] private AudioClip level1Song;
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

    public void interaction()
    {
        if (audioSource != null && interactionSound != null)
        {
            audioSource.PlayOneShot(interactionSound);
        }
    }

    public void jumpingSound()
    {
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
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
        if (audioSource != null && level1Song  != null)
        {
            audioSource.PlayOneShot(level1Song);
        }
    }
}
