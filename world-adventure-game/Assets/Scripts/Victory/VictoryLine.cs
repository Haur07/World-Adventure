using System.Collections;
using UnityEngine;

public class VictoryLine : MonoBehaviour
{
    private bool victoryReached;
    private PlayerBehavior player;
    private Health health;
    private CollectiblesManager collectiblesManager;
    private TimeElapsedManager timeElapsedManager;
    [SerializeField] private GameObject doorInteraction;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject blockerIndicator;

    private void Awake()
    {
        blockerIndicator.SetActive(false);
        victoryReached = false;
        player = FindAnyObjectByType<PlayerBehavior>();
        health = FindAnyObjectByType<Health>();
        collectiblesManager = FindAnyObjectByType<CollectiblesManager>();
        timeElapsedManager = FindAnyObjectByType<TimeElapsedManager>();
        doorInteraction.SetActive(false);
        interactText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !victoryReached)
        {
            victoryReached = true;
            health.SetInvincible();
            // collectiblesManager.SetToBeSavedScore();
            blockerIndicator.SetActive(true);
            timeElapsedManager.SetIsGameRunning(false);
            AudioManager.instance.stopSound();
            AudioManager.instance.victoryTheme();
            StartCoroutine(activateDoorInteraction());
        }
    }

    private IEnumerator activateDoorInteraction()
    {
        yield return new WaitForSeconds(4.5f);
        doorInteraction.SetActive(true);
        interactText.SetActive(true);
    }
}
