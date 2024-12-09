using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Sprite[] jumpChargeSprites;
    [SerializeField] private Image jumpCharge;
    private StairsInteraction stairInteraction;

    private Rigidbody2D body;
    private Collider2D playerCollider;
    private Animator animate;

    private float speed;
    private float jumpSpeed;
    private bool canMove;
    private bool hasJumped;

    private bool onGround;
    private bool onStair;
    private bool interacted;

    private void Awake()
    {
        stairInteraction = FindAnyObjectByType<StairsInteraction>();

        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        
        speed = 5f;
        jumpSpeed = 5f;
        hasJumped = false;
        body.gravityScale = 1f;
        body.freezeRotation = true;
        interacted = false;
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        HandleMovement();
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Correr e pular
        float vertical = Input.GetAxis("Vertical"); // Subir as escadas

        // Direção do personagem
        if (horizontal > 0.01f)
        {
            transform.localScale = new Vector2(5, 5);
        }
        else if (horizontal < -0.01f)
        {
            transform.localScale = new Vector2(-5, 5);
        }

        if(!onStair)
        {
            interacted = false;
            body.velocity = new Vector2(horizontal * speed, body.velocity.y);
            body.gravityScale = 1f;

            if (Input.GetKey(KeyCode.Space) && onGround && !hasJumped)
            {
                StartCoroutine(Jump());
            }
        }
        else
        {
            body.velocity = new Vector2(horizontal * (speed / 3.5f), vertical * (speed / 2.5f));
            body.gravityScale = 0f;
        }

        // Corrida
        animate.SetBool("run", horizontal != 0 && onGround && !onStair);
        animate.SetBool("onGround", onGround && !onStair); // idle

        // Escada
        animate.SetBool("climbing", vertical > 0.01f && !onGround && onStair);
        animate.SetBool("climb-idle", vertical == 0f && !onGround && onStair);
        animate.SetBool("climb-descending", vertical < -0.01f && !onGround && onStair);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
        if (!canMove)
        {
            body.velocity = Vector2.zero;
        }
    }

    private void InstantJumpCharge()
    {
        StopAllCoroutines();
        jumpCharge.sprite = jumpChargeSprites[3];
        hasJumped = false;
    }

    private IEnumerator Jump()
    {
        AudioManager.Instance.PlaySound("jump");
        animate.SetTrigger("jump");
        onGround = false;
        hasJumped = true;
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        
        for (int index = 0; index < 3; index++)
        {
            jumpCharge.sprite = jumpChargeSprites[index];
            yield return new WaitForSeconds(0.3f);
        }

        jumpCharge.sprite = jumpChargeSprites[3];
        hasJumped = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("onGround"))
        {
            onGround = true;
        }
    }

    // Ajuste da velocidade de movimento e pulo
    private void OnTriggerStay2D(Collider2D collision)
    {
        // No solo inclinado
        if (collision.gameObject.CompareTag("SpeedAdjuster"))
        {
            if (Input.GetAxis("Horizontal") > 0.01f)
            {
                speed = 2.5f;
                jumpSpeed = 7;
            }
            else
            {
                speed = 5;
                jumpSpeed = 5;
            }
        }
        else if (collision.gameObject.CompareTag("SpeedAdjusterLow"))
        {
            if (Input.GetAxis("Horizontal") > 0.01f)
            {
                speed = 3.5f;
                jumpSpeed = 5.8f;
            }
            else
            {
                speed = 5;
                jumpSpeed = 5;
            }
        }

        // Nas escadas
        if (stairInteraction != null)
        {
            if (collision.gameObject.CompareTag("Stair") && !interacted)
            {
                stairInteraction.interaction.SetActive(true);
            }

            if (collision.gameObject.CompareTag("Stair") && Input.GetKey(KeyCode.E))
            {
                interacted = true;
                onStair = true;
                onGround = false;
                stairInteraction.interaction.SetActive(false);
                stairInteraction.blockage.SetActive(false);
            }
        }

        if (collision.gameObject.CompareTag("DisablePause"))
        {
            UIManager.Instance.setPauseDisabled(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WallJump"))
        {
            InstantJumpCharge();
        }

        // Lógica de interação com as escadas
        if (stairInteraction != null)
        {
            if (collision.gameObject.CompareTag("StairTop"))
            {
                stairInteraction.blockage.SetActive(true);
            }

            if (collision.gameObject.CompareTag("StairBottom"))
            {
                onGround = true;
                onStair = false;
                animate.SetTrigger("climb-exit");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpeedAdjuster") || collision.gameObject.CompareTag("SpeedAdjusterLow"))
        {
            speed = 5;
            jumpSpeed = 5;
        }

        if (stairInteraction != null)
        {
            if (collision.gameObject.CompareTag("Stair"))
            {
                interacted = false;
                stairInteraction.interaction.SetActive(false);
                onStair = false;
                body.gravityScale = 1;
                animate.SetTrigger("jump");

                if (onGround)
                {
                    animate.SetTrigger("climb-exit");
                }
            }
        }

        if (collision.gameObject.CompareTag("DisablePause"))
        {
            UIManager.Instance.setPauseDisabled(false);
        }
    }
}
