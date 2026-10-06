using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public ObjectPool bulletPool;
    public ObjectPool smokePool;
    public SpriteRenderer enemyBodySprite;
    public SpriteRenderer enemyArmSprite;

    public float speed = 2f;
    public float aimRange = 9f;
    private bool canMove = true;

    public Transform enemyLocation;
    private Transform playerLocation;
    public Transform enemyArmPivot;
    public Transform enemyFirePoint;

    private Vector2 lastPosition;
    private float stuckTimer;

    private Rigidbody2D rb;

    public float fireRate = 1f;
    private float fireCooldown = 0f;

    public Animator animator;

    void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyFlip();

        float distanceToPlayer = Vector2.Distance(enemyLocation.position, playerLocation.position);

        if (distanceToPlayer <= aimRange)
        {
            canMove = false;
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isStopped", true);

            enemyAim();

            if (fireCooldown <= 0f)
            {
                Shooter();
                fireCooldown = 1f / fireRate;
            }
            fireCooldown -= Time.deltaTime;
        }
        else
        {
            canMove = true;
            animator.SetBool("isStopped", false);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MoveToPlayer();
        }
    }

    void MoveToPlayer()
    {
        if (playerLocation)
        {
            Vector2 direction = (playerLocation.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Wall"));

            if (hit.collider != null)
            {
                Vector2 left = new Vector2(-direction.y, direction.x);
                Vector2 right = new Vector2(direction.y, -direction.x);

                bool leftFree = !Physics2D.Raycast(transform.position, left, 1f, LayerMask.GetMask("Wall"));
                bool rightFree = !Physics2D.Raycast(transform.position, right, 1f, LayerMask.GetMask("Wall"));

                if (leftFree)
                    rb.linearVelocity = left * speed;
                else if (rightFree)
                    rb.linearVelocity = right * speed;
                else
                    rb.linearVelocity = -direction * speed;
            }
            else
            {
                rb.linearVelocity = direction * speed;
            }

            HandleStuckFix();
        }
    }

    void enemyAim()
    {
        Vector2 direction = playerLocation.position - enemyArmPivot.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyArmPivot.rotation = Quaternion.Euler(0, 0, angle);
        enemyArmSprite.flipY = direction.x < 0;
    }

    void enemyFlip()
    {
        if (playerLocation.position.x < enemyLocation.position.x)
        {
            enemyBodySprite.flipX = true;
        }
        else
        {
            enemyBodySprite.flipX = false;
        }
    }

    void Shooter()
    {
        bulletPool.Get(enemyFirePoint.position, enemyFirePoint.rotation);
        smokePool.Get(enemyFirePoint.position, enemyFirePoint.rotation);

        SFXManager.Instance.PlayRandom(SFXManager.Instance.enemyGunShots, SFXManager.Instance.gunVolume);
    }

    void HandleStuckFix()
    {
        float movedDistance = Vector2.Distance(transform.position, lastPosition);

        if (movedDistance < 0.01f)
            stuckTimer += Time.deltaTime;
        else
            stuckTimer = 0;

        if (stuckTimer > 0.5f)
        {
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            rb.linearVelocity = randomDir * speed;

            stuckTimer = 0;
        }

        lastPosition = transform.position;
    }
}
