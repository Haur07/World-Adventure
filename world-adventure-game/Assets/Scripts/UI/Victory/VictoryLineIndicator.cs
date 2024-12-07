using UnityEngine;

public class VictoryLineIndicator : MonoBehaviour
{
    [SerializeField] GameObject victoryText;

    private bool victoryReached;
    private PlayerMovement player;
    private Animator animate;

    private void Awake()
    {
        victoryText.SetActive(false);
        victoryReached = false;
        player = FindAnyObjectByType<PlayerMovement>();
        animate = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !victoryReached)
        {
            Health.Instance.StopAllCoroutines();
            victoryReached = true;
            victoryText.SetActive(true);
            victoryText.GetComponent<Animator>().SetTrigger("reached");
            animate.SetBool("reached", true);
            Health.Instance.AlwaysInvincible();
        }
    }
}
