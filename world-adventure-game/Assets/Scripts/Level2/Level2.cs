using System.Collections;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    private PlayerBehavior player;
    private CollectiblesManager collectiblesManager;
    private int score;
    private int sp;
    private int cs;
    private int ts;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerBehavior>();
        collectiblesManager = FindAnyObjectByType<CollectiblesManager>();
        sp = collectiblesManager.GetSelectedPlayer();
        cs = PlayerPrefs.GetInt("CurrentScore" + sp, 0);
        ts = PlayerPrefs.GetInt("Score" + sp, 0);
        score = PlayerPrefs.GetInt("CurrentScore" + collectiblesManager.GetSelectedPlayer(), 0);
    }

    private void Update()
    {
        cs = PlayerPrefs.GetInt("CurrentScore" + sp, 0);
        ts = PlayerPrefs.GetInt("Score" + sp, 0);
        print($"Selected Player: {sp} | Current Score: {cs} | Total Score {ts} | Score: {score}.");
    }
    private void Start()
    {
        AudioManager.instance.level2Theme();

        if (player != null)
        {
            StartCoroutine(FreezePlayer(player, 5.5f));
        }

        if (collectiblesManager != null)
        {
            collectiblesManager.SetCherryPoints(score);
        }
    }

    private IEnumerator FreezePlayer(PlayerBehavior player, float duration)
    {
        player.SetCanMove(false);
        yield return new WaitForSeconds(duration);
        player.SetCanMove(true);
    }
}