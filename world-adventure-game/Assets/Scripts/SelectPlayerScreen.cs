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

    public void Player1()
    {
        PlayerPrefs.SetInt("SelectedPlayer", 1);
        PlayerPrefs.Save();
    }

    public void Player2()
    {
        PlayerPrefs.SetInt("SelectedPlayer", 2);
        PlayerPrefs.Save();
    }

    public void Player3()
    {
        PlayerPrefs.SetInt("SelectedPlayer", 3);
        PlayerPrefs.Save();
    }

    public void ShowSelectPlayerScreen()
    {
        gameObject.SetActive(true);
        playerScreen = true;
    }

    public void HideSelectPlayerScreen()
    {
        gameObject.SetActive(false);
        playerScreen = false;
    }
}
