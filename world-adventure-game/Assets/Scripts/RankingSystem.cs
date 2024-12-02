using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RankingSystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText1;
    [SerializeField] TMP_Text scoreText2;
    [SerializeField] TMP_Text scoreText3;
    private SelectPlayerScreen selectPlayerScreen;
    private bool rankScreen;

    private void Awake()
    {
        gameObject.SetActive(false);
        selectPlayerScreen = FindAnyObjectByType<SelectPlayerScreen>();
        Player1Score();
        Player2Score();
        Player3Score();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && rankScreen)
        {
            HideRankScreen();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScores();
            UpdateScores();
        }
    }

    public void UpdateScores()
    {
        Player1Score();
        Player2Score();
        Player3Score();
    }

    public void ShowRankScreen()
    {
        gameObject.SetActive(true);
        rankScreen = true;
    }

    public void HideRankScreen()
    {
        gameObject.SetActive(false);
        rankScreen= false;
    }

    public void ResetScores()
    {
        PlayerPrefs.SetInt("Score" + 1, 0);
        PlayerPrefs.SetInt("Score" + 2, 0);
        PlayerPrefs.SetInt("Score" + 3, 0);
    }

    private void Player1Score()
    {
        int score = PlayerPrefs.GetInt("Score" + 1, 0);
        string formattedScore = FormatScore(score);
        scoreText1.text = $"{formattedScore}";
    }

    private void Player2Score()
    {
        int score = PlayerPrefs.GetInt("Score" + 2, 0);
        string formattedScore = FormatScore(score);
        scoreText2.text = $"{formattedScore}";
    }

    private void Player3Score()
    {
        int score = PlayerPrefs.GetInt("Score" + 3, 0);
        string formattedScore = FormatScore(score);
        scoreText3.text = $"{formattedScore}";
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
