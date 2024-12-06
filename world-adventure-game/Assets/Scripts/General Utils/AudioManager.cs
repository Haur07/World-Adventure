using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Clips Settings")]
    [SerializeField] private AudioClip interactionSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip menuSong;
    [SerializeField] private AudioClip lobbySong;
    [SerializeField] private AudioClip victorySong;
    [SerializeField] private AudioClip level1Song;
    [SerializeField] private AudioClip level2Song;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip gameOverSong;

    private AudioSource audioSource;
    private Dictionary<string, AudioClip> audioClips;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();

            audioClips = new Dictionary<string, AudioClip>
            {
                { "interaction", interactionSound },
                { "jump", jumpSound },
                { "damage", damageSound },
                { "collect", collectSound },
                { "menu", menuSong },
                { "lobby", lobbySong },
                { "victory", victorySong },
                { "level1", level1Song },
                { "level2", level2Song },
                { "dead", dieSound },
                { "gameover", gameOverSong }
            };
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StopSound()
    {
        StopAllCoroutines();
        audioSource?.Stop(); // '?' só vai executar se for != null
    }

    public void PlaySound(string soundName, bool loop = false)
    {
        if (audioClips.TryGetValue(soundName, out AudioClip clip) && clip != null)
        {
            if (!loop)
            { 
                audioSource.PlayOneShot(clip);
            }
            else
            {
                StartCoroutine(PlaySoundLoop(clip));
            }
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found or is null.");
        }
    }

    private IEnumerator PlaySoundLoop(AudioClip clip)
    {
        while (true)
        {
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length + 3f);
        }
    }
}
