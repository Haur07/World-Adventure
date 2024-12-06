using System.Collections;
using UnityEngine;

public class FreezePlayer : MonoBehaviour
{
    public static FreezePlayer Instance;

    [SerializeField] PlayerBehavior player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Freeze()
    {
        StartCoroutine(DisableMovement(player, 5.5f));
    }

    public IEnumerator DisableMovement(PlayerBehavior player, float duration)
    {
        player.SetCanMove(false);
        yield return new WaitForSeconds(duration);
        player.SetCanMove(true);
    }
}
