using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private AudioClip collectSound;
    private AudioSource audioSource;
    private int cherryPoints;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateScoreDisplay();
    }

    public void SetCherryPoints(int points)
    {
        audioSource.PlayOneShot(collectSound);
        cherryPoints += points;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        string formattedScore = FormatScore(cherryPoints);
        scoreText.text = formattedScore;
    }

    private string FormatScore(int score)
    {
        char[] digits = score.ToString().ToCharArray();
        string formatted = "";

        foreach (char digit in digits)
        {
            formatted += $"<sprite name=num{digit}>";
        }

        return formatted; 
    }
}
