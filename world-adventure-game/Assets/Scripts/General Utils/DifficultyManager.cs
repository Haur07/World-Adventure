using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private Sprite[] easyButtonSprites;
    [SerializeField] private Sprite[] hardButtonSprites;
    [SerializeField] private Image easyButton;
    [SerializeField] private Image hardButton;
    private int easyIndex;
    private int hardIndex;

    private void Awake()
    {
        easyIndex = PlayerPrefs.GetInt("EasyIndex", 0);
        hardIndex = PlayerPrefs.GetInt("HardIndex", 1);

        easyButton.sprite = easyButtonSprites[easyIndex];
        hardButton.sprite = hardButtonSprites[hardIndex];
    }

    public void EasyMode()
    {
        AudioManager.Instance.PlaySound("interaction");
        PlayerPrefs.SetFloat("ThornDamage", 1f);
        PlayerPrefs.SetFloat("FrogDamage", 0.5f);
        PlayerPrefs.SetFloat("HealPlayer", 1f);
        PlayerPrefs.Save();

        easyButton.sprite = easyButtonSprites[1];
        hardButton.sprite = hardButtonSprites[0];
        PlayerPrefs.SetInt("EasyIndex", 1);
        PlayerPrefs.SetInt("HardIndex", 0);
        PlayerPrefs.Save();
    }

    public void HardMode()
    {
        AudioManager.Instance.PlaySound("interaction");
        PlayerPrefs.SetFloat("ThornDamage", 2f);
        PlayerPrefs.SetFloat("FrogDamage", 1f);
        PlayerPrefs.SetFloat("HealPlayer", 0.5f);
        PlayerPrefs.Save();

        easyButton.sprite = easyButtonSprites[0];
        hardButton.sprite = hardButtonSprites[1];
        PlayerPrefs.SetInt("EasyIndex", 0);
        PlayerPrefs.SetInt("HardIndex", 1);
        PlayerPrefs.Save();
    }
}
