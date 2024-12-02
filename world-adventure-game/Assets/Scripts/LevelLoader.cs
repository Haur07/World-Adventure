using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    [SerializeField] private Animator transition;
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

        uiManager.setPausedScreen(false);
        StartCoroutine(LoadLevel(0));   
        StartCoroutine(playerHealth.BecomeInvincible());
        Time.timeScale = 1;
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (levelIndex > 3)
        {
            transition.SetTrigger("start");
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }

        transition.SetTrigger("start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(levelIndex);
    }
}
