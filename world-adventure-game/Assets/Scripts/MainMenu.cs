using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip gameTheme;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (audioSource != null && gameTheme != null)
        {
            audioSource.clip = gameTheme;
            audioSource.Play();
        }
    }
    public void startGame()
    {
        SceneManager.LoadSceneAsync("Scenes/Lobby");
    }
}
