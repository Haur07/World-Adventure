using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private float speed;
    private Rigidbody2D body;
    private Animator animate;
    private bool onGround;
    private bool canMove;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
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
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector2(5, 5);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-5, 5);
        }

        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            Jump();
        }

        animate.SetBool("run", horizontalInput != 0);
        animate.SetBool("onGround", onGround);
    }

    private void Jump()
    {
        AudioManager.instance.jumpingSound();
        body.velocity = new Vector2(body.velocity.x, speed);
        animate.SetTrigger("jump");
        onGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "onGround")
        {
            onGround = true;
        }

        if (collision.gameObject.tag == "rightCollision")
        {
            transform.position = new Vector2(-7.4f, transform.position.y);
        }
        else if (collision.gameObject.tag == "leftCollision")
        {
            transform.position = new Vector2(7.4f, transform.position.y);
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
}
