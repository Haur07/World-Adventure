using UnityEngine;

public class FrogEnemy : MonoBehaviour
{
    private float damage;
    private Health playerHealth;

    private void Awake()
    {
        damage = 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
