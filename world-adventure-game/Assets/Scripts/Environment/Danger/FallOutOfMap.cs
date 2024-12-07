using UnityEngine;

public class FallOutOfMap : MonoBehaviour
{
    private bool dead;

    private void Awake()
    {
        dead = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !dead)
        {
            collision.GetComponent<Health>().InstantKill();
            dead = true;
        }
    }
}
