using UnityEngine;

public class PlayerCollectiblesInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            AudioManager.Instance.PlaySound("collect");
            Destroy(collision.gameObject);
            CollectiblesManager.Instance.SetPoints(10);
        }
        else if (collision.gameObject.CompareTag("Gem"))
        {
            AudioManager.Instance.PlaySound("collect");
            Destroy(collision.gameObject);
            CollectiblesManager.Instance.SetPoints(180);
        }
        else if (collision.gameObject.CompareTag("Heart"))
        {
            AudioManager.Instance.PlaySound("collect");
            Destroy(collision.gameObject);

            if (Health.Instance.GetCurrentHealth() != 3)
            {
                Health.Instance.HealPlayer(PlayerPrefs.GetFloat("HealPlayer", 1));
            }
            else
            {
                CollectiblesManager.Instance.SetPoints(60);
            }
        }
    }
}