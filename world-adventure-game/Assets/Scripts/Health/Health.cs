using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    private PlayerBehavior player;
    private float health;
    private float currentHealth;
    private Animator animate;
    private bool dead;
    private bool isInvincible;
    private float framesDuration;
    private int flashesNumber;
    private SpriteRenderer sprite;
    private UIManager uiManager;
    [SerializeField] private AudioClip gameOverSound;
    private AudioSource audioSource;

    private void Awake()
    {
        health = 3;
        currentHealth = health;
        isInvincible = false;
        framesDuration = 2;
        flashesNumber = 8;
        animate = GetComponent<Animator>();
        player = GetComponent<PlayerBehavior>();
        sprite = GetComponent<SpriteRenderer>();
        uiManager = FindFirstObjectByType<UIManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, health);
        
        if (currentHealth > 0)
        {
            AudioManager.instance.takeDamageSound();
            animate.SetTrigger("hurt");
            StartCoroutine(BecomeInvincible());
        }
        else
        {
            if (!dead)
            {
                AudioManager.instance.stopSound();
                AudioManager.instance.takeDamageSound();
                animate.SetTrigger("die");
                player.SetCanMove(false);
                dead = true;
                StartCoroutine(GameOverScreen()); // Teste. Futuramente irá estar em outro script
            }
        }
    }

    public void SetCurrentHealth(float value)
    {
        currentHealth = value;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        for (int i = 0; i < flashesNumber; i++) {
            sprite.color = new Color(0.7f, 0.7f, 0.7f, 0.3f);
            yield return new WaitForSeconds(framesDuration / (flashesNumber * 2));
            sprite.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(framesDuration / (flashesNumber * 2));
        }
        isInvincible = false;
    }

    private IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(0.65f);
        audioSource.PlayOneShot(gameOverSound);
        yield return new WaitForSeconds(3);
        uiManager.GameOver();
    }
}
