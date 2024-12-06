using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void DoQuitGame()
    {
        AudioManager.Instance.PlaySound("interaction");
        Application.Quit();
    }
}
