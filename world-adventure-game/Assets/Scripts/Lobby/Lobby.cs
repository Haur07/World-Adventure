using System.Collections;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    private PlayerBehavior player;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerBehavior>();
    }

    private void Start()
    {
        AudioManager.instance.lobbyTheme();

        if (player != null)
        {
            StartCoroutine(FreezePlayer(player, 2.35f));
        }
    }

    private IEnumerator FreezePlayer(PlayerBehavior player, float duration)
    {
        player.SetCanMove(false);
        yield return new WaitForSeconds(duration);
        player.SetCanMove(true);
    }
}
