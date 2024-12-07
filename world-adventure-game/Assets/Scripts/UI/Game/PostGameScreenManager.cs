using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostGameScreenManager : MonoBehaviour
{
    public static PostGameScreenManager Instance;

    [SerializeField] private TMP_Text timeValue;
    [SerializeField] private TMP_Text pointsValueNoTime;
    [SerializeField] private TMP_Text pointsValue;
    [SerializeField] private Animator resultsAnimate;
    [SerializeField] private GameObject health;

    private Animator animate;

    private int selectedPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
            gameObject.SetActive(false);
            animate = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowPostGameScreen()
    {
        SetTextsValue();
        CollectiblesManager.Instance.gameObject.SetActive(false);
        TimeElapsedManager.Instance.gameObject.SetActive(false);
        health.SetActive(false);
        StartCoroutine(ShowResults());
    }

    private void SetTextsValue()
    {
        int scoreNoTime = Mathf.Clamp(PlayerPrefs.GetInt("CurrentScoreNoTime" + selectedPlayer, 0), 0, 9999);
        int score = Mathf.Clamp(PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0), 0, 9999);
        string formattedTime = FormatResult(TimeElapsedManager.Instance.GetTimeElapsed());
        string formattedScoreNoTime = FormatResult(scoreNoTime);
        string formattedScore = FormatResult(score);

        timeValue.text = formattedTime;
        pointsValueNoTime.text = formattedScoreNoTime;
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
        if (!Health.Instance.GetIsGameOver())
        {
            LevelLoader.Instance.StartGame();

        }
        else
        {
            StartCoroutine(LevelLoader.Instance.LoadLevel(0));
        }
    }
}
