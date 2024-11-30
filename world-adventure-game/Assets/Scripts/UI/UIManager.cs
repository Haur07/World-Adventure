using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Teste. Futuramente o áudio será realocado para AudioManager
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSong;
    private AudioSource audioSource;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        audioSource.PlayOneShot(gameOverSong);
    }
}
