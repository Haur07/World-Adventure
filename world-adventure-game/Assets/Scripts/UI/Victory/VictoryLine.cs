using System.Collections;
using UnityEngine;

public class VictoryLine : MonoBehaviour
{
    [Header("Door Interaction Settings")]
    [SerializeField] private GameObject doorInteraction;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject blockerIndicator;

    [Header("Game Objects Reference")]
    [SerializeField] private PlayerBehavior player;
    [SerializeField] private CollectiblesManager collectiblesManager;
    [SerializeField] private TimeElapsedManager timeElapsedManager;

    private bool victoryReached;

    private void Awake()
    {
        victoryReached = false;
        doorInteraction.SetActive(false);
        interactText.SetActive(false);
        blockerIndicator.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !victoryReached)
        {
            victoryReached = true;
            StartCoroutine(Health.Instance.AlwaysInvincible());
            timeElapsedManager.SetIsGameRunning(false);
            blockerIndicator.SetActive(true);
            AudioManager.Instance.StopSound();
            AudioManager.Instance.PlaySound("victory", loop: true);
            StartCoroutine(ActivateDoorInteraction());
        }
    }

    private IEnumerator ActivateDoorInteraction()
    {
        yield return new WaitForSeconds(4.5f);
        doorInteraction.SetActive(true);
        interactText.SetActive(true);
    }
}
