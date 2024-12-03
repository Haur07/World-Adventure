using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] GameObject postGameScreenManager;
    [SerializeField] Animator textInteraction;
    public GameObject textSprite;
    private bool isNearDoor = false;
    private bool doorInteracted = false;
    private PlayerBehavior player;
    private CollectiblesManager collectiblesManager;
    private int selectedPlayer;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerBehavior>();
        collectiblesManager = FindAnyObjectByType<CollectiblesManager>();

        if (collectiblesManager != null )
        {
            selectedPlayer = collectiblesManager.GetSelectedPlayer();
        }
    }

    private void Start()
    {
        textSprite.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isNearDoor && Input.GetKeyDown(KeyCode.W) && !doorInteracted)
        {
            if (player != null)
            {
                player.SetCanMove(false);
            }

            if (collectiblesManager != null)
            {
                collectiblesManager.SaveTotalScore();
            }

            doorInteracted = true;
            AudioManager.instance.interaction();

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                LevelLoader.instance.startGame();
            }
            else 
            {
                postGameScreenManager.SetActive(true);
                postGameScreenManager.GetComponent<PostGameScreenManager>().ShowPostGameScreen();
            }
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
