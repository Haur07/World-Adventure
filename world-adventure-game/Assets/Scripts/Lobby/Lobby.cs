using UnityEngine;

public class Lobby : MonoBehaviour
{
    private PlayerMovement player;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerMovement>();

        for (int index = 1; index < 4; index++)
        {
            PlayerPrefs.SetInt("CurrentScore" + index, 0);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlaySound("lobby", loop: true);

        if (player != null)
        {
            StartCoroutine(FreezePlayer.Instance.DisableMovement(player, 2.3f));
        }
        else
        {
            Debug.LogWarning("Player not found or it's null.");
        }
    }
}
