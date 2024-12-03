using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectiblesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int cherryPoints;
    private int removeCherryPoints;
    private int saveCherryPoints;
    private int selectedPlayer;
    private int index;
    private int totalScore;
    private int toBeSavedScore;

    private void Awake()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
        totalScore = PlayerPrefs.GetInt("Score" + selectedPlayer, 0);
        removeCherryPoints = 0;
        cherryPoints = PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0);
        PlayerPrefs.SetInt("RemoveScore", PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0));
        PlayerPrefs.Save();
        UpdateScoreDisplay();
    }

    private void Update()
    {
        totalScore = PlayerPrefs.GetInt("Score" + selectedPlayer, 0);
        toBeSavedScore = cherryPoints - PlayerPrefs.GetInt("RemoveScore", 0);
        removeCherryPoints = toBeSavedScore;

        // Debugging
        // Debug.Log($"Player: {GetSelectedPlayer()} | Level Index: {index} | Total Score: {totalScore} | To Be Saved Score: {toBeSavedScore} | Cherry Points: {cherryPoints}");
    }

    public void SetCherryPoints(int score)
    {
        cherryPoints = Mathf.Max(0, cherryPoints + score);
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

    public int GetSelectedPlayer()
    {
        return selectedPlayer;
    }

    public int GetRemoveCherryPoints()
    {
        return removeCherryPoints;
    }
}
