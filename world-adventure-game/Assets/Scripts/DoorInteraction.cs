using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public GameObject textSprite;
    private bool isNearDoor = false;

    private void Start()
    {
        textSprite.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isNearDoor && Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("Scenes/Level1");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearDoor = true;
            textSprite.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearDoor = false;
            textSprite.gameObject.SetActive(false);
        }
    }
}
