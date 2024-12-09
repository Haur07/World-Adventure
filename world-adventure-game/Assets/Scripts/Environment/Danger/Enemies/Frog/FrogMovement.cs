using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float idleDuration;
    private float idleTimer;
    private float speed;
    private Animator animate;
    private bool movingLeft;
    private Vector2 initScale;

    private void Awake()
    {
        speed = 1.2f;
        initScale = transform.localScale;
        animate = GetComponent<Animator>();
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x >= leftEdge.position.x)
            {
                MoveDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (transform.position.x <= rightEdge.position.x)
            {
                MoveDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        animate.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
        { 
            movingLeft = !movingLeft; 
        }
    }

    private void MoveDirection(int direction)
    {
        idleTimer = 0;
        animate.SetBool("moving", true);
        transform.localScale = new Vector2(Mathf.Abs(initScale.x) * (direction * -1), initScale.y);
        transform.position = new Vector2(transform.position.x + Time.deltaTime * direction * speed, transform.position.y);
    }
}
