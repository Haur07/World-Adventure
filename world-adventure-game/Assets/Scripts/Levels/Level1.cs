using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] private PlayerBehavior player;

    private void Start()
    {
        AudioManager.Instance.PlaySound("level1", loop: true);

        if (player != null)
        {
            StartCoroutine(FreezePlayer.Instance.DisableMovement(player, 5.5f));
        }
        else
        {
            Debug.LogWarning("Player not found or it's null.");
        }
    }
}
