using System.Collections;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    private PlayerBehavior player;
    private CollectiblesManager cm;
    private int sp;
    private int cs;
    private int ts;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerBehavior>();
        cm = FindAnyObjectByType<CollectiblesManager>();
        sp = cm.GetSelectedPlayer();
        cs = PlayerPrefs.GetInt("CurrentScore" + sp, 0);
        ts = PlayerPrefs.GetInt("Score" + sp, 0);
    }

    private void Update()
    {
        cs = PlayerPrefs.GetInt("CurrentScore" + sp, 0);
        ts = PlayerPrefs.GetInt("Score" + sp, 0);
        print($"Selected Player: {sp} | Current Score: {cs} | Total Score {ts}.");
    }
    private void Start()
    {
        AudioManager.instance.level1Theme();

        if (player != null )
        {
            StartCoroutine(FreezePlayer(player, 5.5f));
        }
    }

    private IEnumerator FreezePlayer(PlayerBehavior player, float duration)
    {
        player.SetCanMove(false);
        yield return new WaitForSeconds(duration);
        player.SetCanMove(true);
    }
}
