using UnityEngine;

public class Thorn : MonoBehaviour
{
    private float damage;

    private void Awake()
    {
        damage = PlayerPrefs.GetFloat("ThornDamage", 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
