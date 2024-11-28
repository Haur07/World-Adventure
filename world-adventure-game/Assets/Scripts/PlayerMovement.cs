using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animate;

    private void Start()
    {
        speed = 5f;
        body.gravityScale = 2f;
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        Debug.Log($"horizontalInput: {horizontalInput}");

        // Condição para mudar a direção do personagem ao caminhar
        if (horizontalInput > 0f)
        {
            transform.localScale = new Vector2(5, 5);
        }
        else if (horizontalInput < 0f)
        {
            transform.localScale = new Vector2(-5, 5);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            body.velocity = new Vector2(body.velocity.x, speed);
        }

        // Condição para mudar a animação dependendo da ação
        animate.SetBool("run", horizontalInput != 0);
    }
}
