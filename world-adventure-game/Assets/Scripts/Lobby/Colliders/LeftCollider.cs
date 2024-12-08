using UnityEngine;

public class LeftCollider : MonoBehaviour
{
    private PlayerMovement player;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerMovement>();

        if (player == null)
        {
            Debug.LogWarning("Player object not found or it's null.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = new Vector2(7.4f, player.transform.position.y);
        }
    }
}
