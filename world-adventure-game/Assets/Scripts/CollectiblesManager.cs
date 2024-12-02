using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private AudioClip collectSound;
    private AudioSource audioSource;
    private int cherryPoints;
    private int selectedPlayer;

    private void Awake()
    {
        selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
        audioSource = GetComponent<AudioSource>();
        UpdateScoreDisplay();
    }

    public void SetCherryPoints(int points)
    {
        if (points > 0)
        {
            audioSource.PlayOneShot(collectSound);
        }

        cherryPoints = Mathf.Max(0, cherryPoints + points);
        SaveScore(cherryPoints);
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

    private void SaveScore(int score)
    {
        PlayerPrefs.SetInt("CurrentScore" + selectedPlayer, score);
    }

    public void SaveTotalScore()
    {
        int totalScore = PlayerPrefs.GetInt("Score" + selectedPlayer, 0);
        PlayerPrefs.SetInt("Score" + selectedPlayer, totalScore);
    }

    public int GetSelectedPlayer()
    {
        return selectedPlayer;
    }
}
