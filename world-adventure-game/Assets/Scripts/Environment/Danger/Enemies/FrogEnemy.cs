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
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
