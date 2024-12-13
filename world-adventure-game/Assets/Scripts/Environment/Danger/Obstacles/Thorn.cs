using System.Collections;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    private float damage;

    private void Awake()
    {
        damage = PlayerPrefs.GetFloat("ThornDamage", 1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health.Instance.TakeDamage(damage);
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        StopAllCoroutines();
    //    }
    //}

    //private IEnumerator ApplyDamageOverTime()
    //{
    //    while (true)
    //    {
    //        if (!Health.Instance.GetIsInvincible())
    //        {
    //            Health.Instance.TakeDamage(damage);
    //        }
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}
}
