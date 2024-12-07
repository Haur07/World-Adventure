using TMPro;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager Instance;

    [SerializeField] private TMP_Text scoreText;

    private int selectedPlayer;
    private int points;
    private int toBeSavedScore;
    private int totalScore;
    private int time;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
            points = Mathf.Clamp(PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0), 0, 9999);
            totalScore = PlayerPrefs.GetInt("Score" + selectedPlayer, 0);
            time = 0;
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
        time = TimeElapsedManager.Instance.GetTimeElapsed();
        totalScore = PlayerPrefs.GetInt("Score" + selectedPlayer, 0);
        toBeSavedScore = points - PlayerPrefs.GetInt("RemoveScore", 0);

        // Debugging
        Debug.Log($"Selected Player: {selectedPlayer} | Invincibility: {Health.Instance.GetIsInvincible()} | Score: {points} | To Be Saved Score (-time x3) {toBeSavedScore - time * 3} | Total Score: {totalScore} | Time Elapsed (x3): {time * 3}");
    }

    public void ResetCurrentPoints()
    {
        int currentPoints = PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0);
        int removePoints = Mathf.Max(0, toBeSavedScore);
        PlayerPrefs.SetInt("CurrentScore" + selectedPlayer, currentPoints - removePoints);
        PlayerPrefs.Save();
    }

    public void SetPoints(int score)
    {
        points = Mathf.Clamp(points + score, 0, 9999);
        SaveCurrentScore(points);
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        string formattedScore = FormatScore(points);
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
        PlayerPrefs.SetInt("Score" + selectedPlayer, Mathf.Clamp(totalScore + (toBeSavedScore - time * 3), 0, 9999));
        PlayerPrefs.Save();
    }
}
