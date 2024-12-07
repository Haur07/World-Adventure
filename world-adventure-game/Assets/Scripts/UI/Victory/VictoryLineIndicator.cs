using UnityEngine;

public class VictoryLineIndicator : MonoBehaviour
{
    [SerializeField] GameObject victoryText;
    [SerializeField] GameObject invincibilityField;

    private bool victoryReached;
    private PlayerBehavior player;
    private Animator animate;

    private void Awake()
    {
        victoryText.SetActive(false);
        invincibilityField.SetActive(false);
        victoryReached = false;
        player = FindAnyObjectByType<PlayerBehavior>();
        animate = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !victoryReached)
        {
            victoryReached = true;
            victoryText.SetActive(true);
            invincibilityField.SetActive(true);
            victoryText.GetComponent<Animator>().SetTrigger("reached");
            animate.SetBool("reached", true);
        }
    }
}
