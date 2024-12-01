using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] Animator textInteraction;
    public GameObject textSprite;
    private bool isNearDoor = false;
    private PlayerBehavior player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerBehavior>();
    }

    private void Start()
    {
        textSprite.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isNearDoor && Input.GetKeyDown(KeyCode.W))
        {
            AudioManager.instance.interaction();
            LevelLoader.instance.startGame();

            if (player != null)
            {
                player.SetCanMove(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textInteraction.SetTrigger("text-visible");
            isNearDoor = true;
            textSprite.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textInteraction.SetTrigger("text-invisible");
            isNearDoor = false;
            textSprite.gameObject.SetActive(false);
        }
    }
}
