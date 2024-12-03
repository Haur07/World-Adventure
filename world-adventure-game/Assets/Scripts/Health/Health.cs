using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private CollectiblesManager collectiblesManager;
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
        collectiblesManager = FindAnyObjectByType<CollectiblesManager>();
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
            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                if (damage < 1)
                {
                    collectiblesManager.SetCherryPoints(-2);
                }
                else
                {
                    collectiblesManager.SetCherryPoints((int)damage * 4 * -1);
                }
            }

            AudioManager.instance.takeDamageSound();
            animate.SetTrigger("hurt");
            player.SetCanMove(false);
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

                if (SceneManager.GetActiveScene().buildIndex != 1)
                {
                    collectiblesManager.SetCherryPoints(-1000);
                    PlayerPrefs.SetInt("Score" + collectiblesManager.GetSelectedPlayer(), 0);
                    PlayerPrefs.Save();
                }
                
                StartCoroutine(GameOverScreen()); // Teste. Futuramente irá estar em outro script
            }
        }
    }

    public void InstantKill()
    {
        currentHealth = 0;
        AudioManager.instance.stopSound();
        player.SetCanMove(false);
        dead = true;

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            collectiblesManager.SetCherryPoints(-1000);
            PlayerPrefs.SetInt("Score" + collectiblesManager.GetSelectedPlayer(), 0);
            PlayerPrefs.Save();
        }

        StartCoroutine(InstantGameOverScreen());
    }

    public void SetCurrentHealth(float value)
    {
        currentHealth = value;
    }

    public void SetInvincible()
    {
        StartCoroutine(AlwaysInvincible());
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.6f);
        player.SetCanMove(true);
        for (int i = 0; i < flashesNumber; i++) {
            sprite.color = new Color(0.7f, 0.7f, 0.7f, 0.3f);
            yield return new WaitForSeconds(framesDuration / (flashesNumber * 2));
            sprite.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(framesDuration / (flashesNumber * 2));
        }
        isInvincible = false;
    }

    public IEnumerator AlwaysInvincible()
    {
        while (true)
        {
            isInvincible = true;
            sprite.color = new Color(1, 1, 0.3f, 0.8f);
            yield return new WaitForSeconds(framesDuration / (flashesNumber * 2));
            sprite.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(framesDuration / (flashesNumber * 2));
        }
    }

    private IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(0.65f);
        audioSource.PlayOneShot(gameOverSound);
        yield return new WaitForSeconds(3);
        uiManager.GameOver();
    }

    private IEnumerator InstantGameOverScreen()
    {
        audioSource.PlayOneShot(gameOverSound);
        yield return new WaitForSeconds(3);
        uiManager.GameOver();
    }
}
