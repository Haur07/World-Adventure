using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeElapsedManager : MonoBehaviour
{
    public static TimeElapsedManager Instance;

    [SerializeField] TMP_Text timeElapsed;

    private int timeElapsedText;
    private bool withinTimeLimit;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            timeElapsed.gameObject.SetActive(false);
            withinTimeLimit = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            timeElapsed.gameObject.SetActive(true);
            StartCoroutine(TimeRoutine());
        }
    }

    private IEnumerator TimeRoutine()
    {
        yield return new WaitForSeconds(5.5f);
        while (withinTimeLimit)
        {
            yield return new WaitForSeconds(1);
            timeElapsedText += 1;
            timeElapsed.text = FormatText(timeElapsedText);

            if (timeElapsedText >= 999)
            {
                withinTimeLimit = false;
                Health.Instance.StopAllCoroutines();
                Health.Instance.TimeExceededDeath();
            }
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

    public int GetTimeElapsed()
    {
        return timeElapsedText;
    }
}
