using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{
    [SerializeField] GameObject confirmationScreen;
    [SerializeField] TMP_Text scoreText1;
    [SerializeField] TMP_Text scoreText2;
    [SerializeField] TMP_Text scoreText3;

    private bool rankScreen;

    private void Awake()
    {
        gameObject.SetActive(false);
        confirmationScreen.SetActive(false);
        PlayersScores();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && rankScreen)
        {
            HideRankScreen();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ShowConfirmationScreen();
        }
    }

    public void ShowRankScreen()
    {
        AudioManager.Instance.PlaySound("interaction");
        gameObject.SetActive(true);
        rankScreen = true;
    }

    public void HideRankScreen()
    {
        AudioManager.Instance.PlaySound("interaction");
        gameObject.SetActive(false);
        rankScreen= false;
    }

    public void ShowConfirmationScreen()
    {
        AudioManager.Instance.PlaySound("interaction");
        confirmationScreen.SetActive(true);
    }

    public void HideConfirmationScreen()
    {
        AudioManager.Instance.PlaySound("interaction");
        confirmationScreen.SetActive(false);
    }

    public void ResetScores()
    {
        AudioManager.Instance.PlaySound("interaction");
        confirmationScreen.SetActive(false);

        for (int index = 1; index < 4; index++)
        {
            PlayerPrefs.SetInt("Score" + index, 0);
            PlayerPrefs.Save();
        }

        PlayersScores();
    }

    private void PlayersScores()
    {
        List<TMP_Text> textList = new List<TMP_Text> { scoreText1, scoreText2, scoreText3 };

        for (int index = 1; index < 4; index++)
        {
            int score = PlayerPrefs.GetInt("Score" + index, 0);
            string formattedScore = FormatScore(score);
            textList[index - 1].text = $"{formattedScore}";
        }
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
