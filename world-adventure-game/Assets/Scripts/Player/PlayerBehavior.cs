using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private CollectiblesManager collectiblesManager;
    [SerializeField] private GameObject interaction;
    [SerializeField] private GameObject blockage; 

    private Rigidbody2D body;
    private Animator animate;

    private float speed;
    private bool onGround;
    private bool onStair;
    private bool canMove;
    private bool interacted;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        interacted = false;

        if (interaction != null)
        {
            interaction.SetActive(false);
        }
        if (blockage != null)
        {
            blockage.SetActive(false);
        }
    }

    private void Start()
    {
        speed = 5f;
        body.gravityScale = 1f;
        body.freezeRotation = true;
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
            
        if (!onStair)
        {
            interacted = false;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            body.gravityScale = 1;

            if (Input.GetKey(KeyCode.Space) && onGround)
            {
                Jump();
            }
        }
        else
        {
            body.velocity = new Vector2(horizontalInput * (speed / 3.5f), verticalInput * (speed / 2.5f));
            body.gravityScale = 0;
        }

        // Debugging
        // Debug.Log($"On Ground: {onGround} | On Stair {onStair} | Vertical Input {verticalInput}");

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector2(5, 5);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-5, 5);
        }

        animate.SetBool("run", horizontalInput != 0 && onGround && !onStair);
        animate.SetBool("onGround", onGround && !onStair);

        animate.SetBool("climbing", verticalInput > 0.01f && onStair);
        animate.SetBool("climb-idle", verticalInput == 0 && onStair);
        animate.SetBool("climb-descending", verticalInput < -0.01f && onStair);
    }

    private void Jump()
    {
        AudioManager.Instance.PlaySound("jump");
        body.velocity = new Vector2(body.velocity.x, speed);
        onGround = false;
        animate.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("onGround"))
        {
            onGround = true;
        }

        if (collision.gameObject.CompareTag("rightCollision"))
        {
            transform.position = new Vector2(-7.4f, transform.position.y);
        }
        else if (collision.gameObject.CompareTag("leftCollision"))
        {
            transform.position = new Vector2(7.4f, transform.position.y);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("onGround"))
        {
            onGround = false;

            if (!onStair)
            {
                animate.SetTrigger("jump");
            }
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
        if (!canMove)
        {
            body.velocity = Vector2.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stair") && !interacted)
        {
            interaction.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Stair") && Input.GetKey(KeyCode.E))
        {
            interacted = true;
            interaction.SetActive(false);
            blockage.SetActive(false);
            onStair = true;
            onGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StairTop"))
        {
            blockage.SetActive(true);
        }

        if (collision.gameObject.CompareTag("StairBottom"))
        {
            onGround = true;
            onStair = false;
            animate.SetTrigger("climb-exit");
        }

        if(collision.gameObject.CompareTag("Cherry"))
        {
            AudioManager.Instance.PlaySound("collect");
            Destroy(collision.gameObject);
            collectiblesManager.SetPoints(20);
        }
        else if (collision.gameObject.CompareTag("Gem"))
        {
            AudioManager.Instance.PlaySound("collect");
            Destroy(collision.gameObject);
            collectiblesManager.SetPoints(300);
        }
        else if (collision.gameObject.CompareTag("Heart"))
        {
            AudioManager.Instance.PlaySound("collect");
            Destroy(collision.gameObject);

            if (Health.Instance.GetCurrentHealth() != 3)
            {
                Health.Instance.HealPlayer(PlayerPrefs.GetFloat("HealPlayer", 1));
            }
            else
            {
                collectiblesManager.SetPoints(80);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stair"))
        {
            interacted = false;
            interaction.SetActive(false);
            onStair = false;
            body.gravityScale = 1;
            animate.SetTrigger("jump");

            if (onGround)
            {
                animate.SetTrigger("climb-exit");
            }
        }
    }
}
