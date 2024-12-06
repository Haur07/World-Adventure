using UnityEngine;

public class Level3 : MonoBehaviour
{
    [SerializeField] private PlayerBehavior player;

    private void Start()
    {
        AudioManager.Instance.PlaySound("level3", loop: true);

        if (player != null)
        {
            FreezePlayer.Instance.Freeze();
        }
        else
        {
            Debug.LogWarning("Player not found or it's null.");
        }
    }
}