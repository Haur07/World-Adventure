using System.Collections;
using TMPro;
using UnityEngine;

public class PostGameScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsValue;
    [SerializeField] private TMP_Text timeValue;
    [SerializeField] private Animator resultsAnimate;
    [SerializeField] private GameObject health;
    private CollectiblesManager collectiblesManager;
    private TimeElapsedManager timeElapsedManager;
    private Animator animate;

    private void Awake()
    {
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
        int score = PlayerPrefs.GetInt("CurrentScore" + collectiblesManager.GetSelectedPlayer(), 0);
        string formattedScore = FormatResult(score);
        string formattedTime = FormatResult(timeElapsedManager.GetTimeElapsed());

        pointsValue.text = formattedScore;
        timeValue.text = formattedTime;
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
        LevelLoader.instance.startGame();
    }
}
