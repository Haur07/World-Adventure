using TMPro;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager Instance;

    [SerializeField] private TMP_Text scoreText;

    private int selectedPlayer;
    private int cherryPoints;
    private int saveCherryPoints;
    private int toBeSavedScore;
    private int totalScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
            cherryPoints = Mathf.Clamp(PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0), 0, 9999);
            totalScore = PlayerPrefs.GetInt("Score" + selectedPlayer, 0);
            PlayerPrefs.SetInt("RemoveScore", PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0));
            PlayerPrefs.Save();
            UpdateScoreDisplay();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        totalScore = PlayerPrefs.GetInt("Score" + selectedPlayer, 0);
        toBeSavedScore = cherryPoints - PlayerPrefs.GetInt("RemoveScore", 0);

        Debug.Log($"Selected Player: {selectedPlayer} | Cherry Points: {cherryPoints} | To Be Saved Score {toBeSavedScore} | Total Score: {totalScore}");
    }

    public void ResetCurrentPoints()
    {
        int currentPoints = PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0);
        int removePoints = Mathf.Max(0, toBeSavedScore);
        PlayerPrefs.SetInt("CurrentScore" + selectedPlayer, currentPoints - removePoints);
        PlayerPrefs.Save();
    }

    public void SetCherryPoints(int score)
    {
        cherryPoints = Mathf.Clamp(cherryPoints + score, 0, 9999);
        SaveCurrentScore(cherryPoints);
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

    private void SaveCurrentScore(int score)
    {
        PlayerPrefs.SetInt("CurrentScore" + selectedPlayer, score);
        PlayerPrefs.Save();
    }

    public void SaveTotalScore()
    {
        PlayerPrefs.SetInt("Score" + selectedPlayer, Mathf.Min(9999, totalScore + toBeSavedScore));
        PlayerPrefs.Save();
    }
}
