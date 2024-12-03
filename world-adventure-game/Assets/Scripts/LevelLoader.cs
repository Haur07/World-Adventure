using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    [SerializeField] private Animator transition;
    private CollectiblesManager collectiblesManager;
    public UIManager uiManager;
    public Health playerHealth;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        uiManager = FindFirstObjectByType<UIManager>();
        playerHealth = FindAnyObjectByType<Health>();
        collectiblesManager = FindAnyObjectByType<CollectiblesManager>();
    }

    public void resetPoints()
    {
        PlayerPrefs.SetInt("RemoveScore", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("CurrentScore" + 1, 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("CurrentScore" + 2, 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("CurrentScore" + 3, 0);
        PlayerPrefs.Save();
    }

    public void resetCurrentPoints()
    {
        int selectedPlayer = collectiblesManager.GetSelectedPlayer();
        int currentPoints = PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0);
        int removePoints = Mathf.Max(0, collectiblesManager.GetRemoveCherryPoints());
        PlayerPrefs.SetInt("CurrentScore" + selectedPlayer,  currentPoints - removePoints);
        PlayerPrefs.Save();
    }

    public void startGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void restartGame()
    {
        uiManager.setPausedScreen(false);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        StartCoroutine(playerHealth.BecomeInvincible());
        Time.timeScale = 1;
    }

    public void mainMenu()
    {
        if (uiManager.getGameOverScreen())
        {
            uiManager.setGameOverScreen(false);
        }

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            PlayerPrefs.SetInt("CurrentScore" + collectiblesManager.GetSelectedPlayer(), 0);
            PlayerPrefs.Save();
        }
        
        uiManager.setPausedScreen(false);
        StartCoroutine(LoadLevel(0));   
        StartCoroutine(playerHealth.BecomeInvincible());
        Time.timeScale = 1;
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (levelIndex > 3)
        {
            PlayerPrefs.SetInt("CurrentScore" + collectiblesManager.GetSelectedPlayer(), 0);
            PlayerPrefs.Save();
            transition.SetTrigger("start");
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }

        transition.SetTrigger("start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(levelIndex);
    }
}
