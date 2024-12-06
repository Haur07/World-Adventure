using System.Collections;
using UnityEngine;

public class FrogEnemy : MonoBehaviour
{
    private float damage;

    private void Awake()
    {
        damage = PlayerPrefs.GetFloat("FrogDamage", 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ApplyDamageOverTime());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        while (true)
        {
            if (!Health.Instance.GetIsInvincible())
            {
                Health.Instance.TakeDamage(damage);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
