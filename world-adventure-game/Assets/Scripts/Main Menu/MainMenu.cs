using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlaySound("menu", loop: true);

        for (int index = 1; index < 4; index++)
        {
            PlayerPrefs.SetInt("CurrentScore" + index, 0);
            PlayerPrefs.Save();
        }
    }
}
