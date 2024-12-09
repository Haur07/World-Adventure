using System.Collections;
using UnityEngine;

public class FrogEnemy : MonoBehaviour
{
    private float damage;

    private void Awake()
    {
        damage = PlayerPrefs.GetFloat("FrogDamage", 0.5f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health.Instance.TakeDamage(damage);
        }
    }
}
