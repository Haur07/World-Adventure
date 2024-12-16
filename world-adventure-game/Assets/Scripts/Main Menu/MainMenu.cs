using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject introScreen;
    private void Start()
    {
        AudioManager.Instance.PlaySound("menu", loop: true);
        StartCoroutine(DisableIntroScreen());

        for (int index = 1; index < 4; index++)
        {
            PlayerPrefs.SetInt("CurrentScore" + index, 0);
            PlayerPrefs.Save();
        }
    }

    private IEnumerator DisableIntroScreen()
    {
        yield return new WaitForSecondsRealtime(9);
        introScreen.SetActive(false);
    }
}
