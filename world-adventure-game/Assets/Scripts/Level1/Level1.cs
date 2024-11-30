using System.Collections;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    private PlayerBehavior player;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerBehavior>();
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
