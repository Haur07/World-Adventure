using UnityEngine;

public class Lobby : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.lobbyTheme();
        PlayerBehavior player = FindAnyObjectByType<PlayerBehavior>();

        if (player != null)
        {
            player.SetCanMove(true);
        }
    }
}
