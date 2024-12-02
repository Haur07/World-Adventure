using System.Collections;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    private PlayerBehavior player;
    private CollectiblesManager collectiblesManager;
    private int score;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerBehavior>();
        collectiblesManager = FindAnyObjectByType<CollectiblesManager>();
        score = PlayerPrefs.GetInt("CurrentScore" + collectiblesManager.GetSelectedPlayer(), 0);
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