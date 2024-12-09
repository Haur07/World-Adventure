using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform enemy;
    [SerializeField] private Animator animate;
    [SerializeField] private float idleDuration;

    private float idleTimer;
    private float speed;
    private bool movingLeft;
    private Vector2 initScale;

    private void Awake()
    {
        speed = 1.8f;
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        animate.SetBool("moving", false);
        
    }

    private void Update()
    {

        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
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
            if (enemy.position.x <= rightEdge.position.x)
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

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }


    private void MoveDirection(int direction)
    {
        idleTimer = 0;
        animate.SetBool("moving", true);
        enemy.localScale = new Vector2(Mathf.Abs(initScale.x) * (direction * -1), initScale.y);
        enemy.position = new Vector2(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y);
    }
}
