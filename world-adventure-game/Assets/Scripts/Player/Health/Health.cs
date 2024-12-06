using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static Health Instance;

    [SerializeField] private PlayerBehavior player;

    private Animator animate;
    private SpriteRenderer sprite;

    private bool isGameOver;
    private int selectedPlayer;
    private bool isInvincible;
    private bool dead;
    private float health;
    private float currentHealth;
    private float framesDuration;
    private int flashesNumber;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            isGameOver = false;
            selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
            isInvincible = false;
            health = 3;
            currentHealth = health;
            framesDuration = 2;
            flashesNumber = 8;
            animate = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
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
                    CollectiblesManager.Instance.SetCherryPoints(-2);
                }
                else
                {
                    CollectiblesManager.Instance.SetCherryPoints((int)damage * 4 * -1);
                }
            }

            AudioManager.Instance.PlaySound("damage");
            animate.SetTrigger("hurt");
            player.SetCanMove(false);
            StartCoroutine(BecomeInvincible());
        }
        else
        {
            if (!dead)
            {
                AudioManager.Instance.StopSound();
                AudioManager.Instance.PlaySound("damage");
                animate.SetTrigger("die");
                player.SetCanMove(false);
                dead = true;
                StartCoroutine(GameOverScreen()); // Teste. Futuramente irá estar em outro script
            }
        }
    }

    public void InstantKill()
    {
        if (!isInvincible)
        {
            currentHealth = 0;
            AudioManager.Instance.StopSound();
            player.SetCanMove(false);
            dead = true;
            StartCoroutine(InstantGameOverScreen());
        }
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
        isGameOver = true;
        yield return new WaitForSeconds(0.65f);
        AudioManager.Instance.PlaySound("dead");
        yield return new WaitForSeconds(3);
        UIManager.Instance.GameOver();
    }

    private IEnumerator InstantGameOverScreen()
    {
        isGameOver = true;
        AudioManager.Instance.PlaySound("dead");
        yield return new WaitForSeconds(3);
        UIManager.Instance.GameOver();
    }
}
