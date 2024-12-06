using UnityEngine;
using UnityEngine.UI;

public class SelectPlayerScreen : MonoBehaviour
{
    private bool playerScreen;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerScreen)
        {
            HideSelectPlayerScreen();
        }
    }

    public void PlayerSelection(int index)
    {
        AudioManager.Instance.PlaySound("interaction");
        PlayerPrefs.SetInt("SelectedPlayer", index);
        PlayerPrefs.Save();
    }

    public void ShowSelectPlayerScreen()
    {
        AudioManager.Instance.PlaySound("interaction");
        gameObject.SetActive(true);
        playerScreen = true;
    }

    public void HideSelectPlayerScreen()
    {
        AudioManager.Instance.PlaySound("interaction");
        gameObject.SetActive(false);
        playerScreen = false;
    }
}
