using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] Animator textInteraction;
    public GameObject textSprite;
    private bool isNearDoor = false;
    private PlayerBehavior player;
    private CollectiblesManager collectiblesManager;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerBehavior>();
        collectiblesManager = FindAnyObjectByType<CollectiblesManager>();
    }

    private void Start()
    {
        textSprite.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isNearDoor && Input.GetKeyDown(KeyCode.W))
        {
            if (player != null)
            {
                player.SetCanMove(false);
            }

            if (collectiblesManager != null)
            {
                int selectedPlayer = collectiblesManager.GetSelectedPlayer();
                int currentScore = PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0);
                int score = PlayerPrefs.GetInt("Score" + selectedPlayer, 0);
                PlayerPrefs.SetInt("Score" + selectedPlayer, currentScore + score);
                collectiblesManager.SaveTotalScore();
            }

            AudioManager.instance.interaction();
            LevelLoader.instance.startGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            textInteraction.SetTrigger("text-visible");
            isNearDoor = true;
            textSprite.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            textInteraction.SetTrigger("text-invisible");
            isNearDoor = false;
            textSprite.gameObject.SetActive(false);
        }
    }
}
