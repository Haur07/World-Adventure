using System.Collections;
using UnityEngine;

public class VictoryLine : MonoBehaviour
{
    private bool victoryReached;
    private PlayerBehavior player;
    [SerializeField] private GameObject doorInteraction;
    [SerializeField] private GameObject interactText;

    private void Awake()
    {
        victoryReached = false;
        player = FindAnyObjectByType<PlayerBehavior>();
        doorInteraction.SetActive(false);
        interactText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !victoryReached)
        {
            victoryReached = true;
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
