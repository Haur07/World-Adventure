using UnityEngine;

public class FallOutOfMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().InstantKill();
        }
    }
}