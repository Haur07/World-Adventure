using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    private SlimeMovement slimeMovement;
    private Health playerHealth;

    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] LayerMask playerLayer;
    private Animator animate;

    [SerializeField] private float range;
    [SerializeField] private float height;
    [SerializeField] private float colliderDistance;

    private float attackCooldown;
    private float damage;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        slimeMovement = GetComponentInParent<SlimeMovement>();
        animate = GetComponent<Animator>();
        attackCooldown = 2f;
        damage = 0.25f;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(PlayerInSight() && !Health.Instance.GetIsGameOver())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animate.SetTrigger("attack");
            }
        }

        if (slimeMovement != null)
        {
            slimeMovement.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * (-range) * transform.localScale.x * colliderDistance, 
                                             new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * height), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * (-range) * transform.localScale.x * colliderDistance, 
                            new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * height));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            Health.Instance.TakeDamage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health.Instance.TakeDamage(damage);
        }
    }
}
