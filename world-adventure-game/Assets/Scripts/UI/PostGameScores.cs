using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostGameScores : MonoBehaviour
{
    [SerializeField] TMP_Text timeElapsed;
    private int timeElapsedText;
    private bool isGameRunning;

    private void Start()
    {
        isGameRunning = true;

        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            StartCoroutine(TimeRoutine());
        }
    }

    private IEnumerator TimeRoutine()
    {
        yield return new WaitForSeconds(5.5f);
        while(isGameRunning)
        {
            yield return new WaitForSeconds(1);
            timeElapsedText += 1;
            timeElapsed.text = FormatText(timeElapsedText);
        }
    }

    private string FormatText(int value)
    {
        char[] digits = value.ToString().ToCharArray();
        string formatted = "";

        foreach (char digit in digits)
        {
            formatted += $"<sprite name=num{digit}>";
        }

        return formatted;
    }
    public void SetIsGameRunning(bool value)
    {
        isGameRunning = value;
    }
}
