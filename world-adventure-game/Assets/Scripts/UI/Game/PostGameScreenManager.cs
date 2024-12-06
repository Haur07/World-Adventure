using System.Collections;
using TMPro;
using UnityEngine;

public class PostGameScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timeValue;
    [SerializeField] private TMP_Text pointsValueNoTime;
    [SerializeField] private TMP_Text minusPointsValue;
    [SerializeField] private TMP_Text pointsValue;
    [SerializeField] private Animator resultsAnimate;
    [SerializeField] private GameObject health;
    private CollectiblesManager collectiblesManager;
    private TimeElapsedManager timeElapsedManager;
    private Animator animate;
    private int selectedPlayer;

    private void Awake()
    {
        selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
        gameObject.SetActive(false);
        collectiblesManager = FindAnyObjectByType<CollectiblesManager>();
        timeElapsedManager = FindAnyObjectByType<TimeElapsedManager>();
        animate = GetComponent<Animator>();
    }

    public void ShowPostGameScreen()
    {
        SetTextsValue();
        collectiblesManager.gameObject.SetActive(false);
        timeElapsedManager.gameObject.SetActive(false);
        health.SetActive(false);
        StartCoroutine(ShowResults());
    }

    private void SetTextsValue()
    {
        int scoreNoTime = PlayerPrefs.GetInt("CurrentScoreNoTime" + selectedPlayer, 0);
        int score = PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0);
        string formattedTime = FormatResult(timeElapsedManager.GetTimeElapsed());
        string formattedScoreNoTime = FormatResult(scoreNoTime);
        string formattedMinusScore = FormatResult(timeElapsedManager.GetTimeElapsed() * 3);
        string formattedScore = FormatResult(score);

        timeValue.text = formattedTime;
        pointsValueNoTime.text = formattedScoreNoTime;
        minusPointsValue.text = formattedMinusScore;
        pointsValue.text = formattedScore;
    }

    private string FormatResult(int value)
    {
        char[] digits = value.ToString().ToCharArray();
        string formatted = "";

        foreach (char digit in digits)
        {
            formatted += $"<sprite name=num{digit}>";
        }

        return formatted;
    }

    private IEnumerator ShowResults()
    {
        animate.SetTrigger("showScreen");
        yield return new WaitForSeconds(2);
        resultsAnimate.SetTrigger("showResults");
        yield return new WaitForSeconds(5);
        LevelLoader.Instance.StartGame();
    }
}
