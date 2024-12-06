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

    private bool victoryReached;
    private int selectedPlayer;
    private int currentScore;
    private int time;

    private void Awake()
    {
        victoryReached = false;
        selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
        currentScore = 0;
        time = 0;
        doorInteraction.SetActive(false);
        interactText.SetActive(false);
        blockerIndicator.SetActive(false);
    }

    private void Update()
    {
        currentScore = PlayerPrefs.GetInt("CurrentScore" + selectedPlayer);
        time = TimeElapsedManager.Instance.GetTimeElapsed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !victoryReached)
        {
            victoryReached = true;
            StartCoroutine(Health.Instance.AlwaysInvincible());
            blockerIndicator.SetActive(true);
            TimeElapsedManager.Instance.StopAllCoroutines();
            AudioManager.Instance.StopSound();
            AudioManager.Instance.PlaySound("victory", loop: true);
            PlayerPrefs.SetInt("CurrentScoreNoTime" + selectedPlayer, currentScore);
            PlayerPrefs.SetInt("CurrentScore" + selectedPlayer, currentScore - time * 3);
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
