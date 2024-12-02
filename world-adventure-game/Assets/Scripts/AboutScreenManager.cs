using UnityEngine;

public class AboutScreenManager : MonoBehaviour
{
    private bool aboutScreen;
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && aboutScreen)
        {
            HideAboutScreen();
        }
    }

    public void ShowAboutScreen()
    {
        gameObject.SetActive(true);
        aboutScreen = true;
    }

    public void HideAboutScreen()
    {
        gameObject.SetActive(false);
        aboutScreen = false;
    }
}
