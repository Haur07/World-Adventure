using UnityEngine;

public class Thorn : MonoBehaviour
{
    private float damage;

    private void Awake()
    {
        damage = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
