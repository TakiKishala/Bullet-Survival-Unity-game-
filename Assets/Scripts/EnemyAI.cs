//using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public static EnemyAI Instance;

    public GameObject bulletPrefab;
    public GameObject smokePrefab;
    public SpriteRenderer enemyBodySprite;
    public SpriteRenderer enemyArmSprite;

    public float speed = 2f; 
    public float aimRange = 9f; 
    private bool canMove = true; 

    public Transform enemyLocation; // Reference to the enemy's transform
    private Transform playerLocation; // Reference to the player's transform
    public Transform enemyArmPivot; // Reference to the pivot point of the enemy's arm
    public Transform enemyFirePoint;

    private Vector2 lastPosition; // To track the last position for stuck detection
    private float stuckTimer; // Timer to track how long the enemy has been stuck

    private Rigidbody2D rb; 

    public float fireRate = 1f; // Number of shots per second
    private float fireCooldown = 0f; // Cooldown timer for shooting
    public float stopDistance = 5f;
    public float resumeDistance = 6f;

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

            canMove = false;// Stop moving when within aim range
            rb.linearVelocity = Vector2.zero; // Stop the enemy's movement
            animator.SetBool("isStopped", true); // Trigger the aiming animation

            enemyAim();

            if(fireCooldown <= 0f)
            {
                Shooter(); 
                fireCooldown = 1f / fireRate; // Reset the cooldown 
            } 
            fireCooldown -= Time.deltaTime; // Decrease the cooldown timer

        }
        else
        {
                canMove = true; // Resume moving when outside of aim range
                animator.SetBool("isStopped", false); // Resume movement animation
        }
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            MoveToPlayer();
        }
    }

    // Method to move the enemy towards the player
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
                    rb.linearVelocity = -direction * speed;           // Vector2.zero; // stuck
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

        //Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        //enemyArmPivot.rotation = Quaternion.Lerp(enemyArmPivot.rotation, targetRotation, Time.deltaTime * 10f);

    }
    void enemyFlip()
    {
        Vector2 direction = playerLocation.position - enemyArmPivot.position;
        if (playerLocation.position.x < enemyLocation.position.x)
        {
            enemyBodySprite.flipX = true; // Player is left

            //enemyArmSprite.flipX = true;
        }
        else
        {
            enemyBodySprite.flipX = false; // Player is right

            //enemyArmSprite.flipX = false;
        }
        //enemyArmSprite.flipY = direction.x < 0;
    }
    void Shooter()
    {
        // Implement shooting logic here (e.g., instantiate a bullet, play a shooting animation, etc.)
        Instantiate(bulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
        GameObject smoke = Instantiate(smokePrefab, enemyFirePoint.position, enemyFirePoint.rotation);


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
            // force escape direction
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            rb.linearVelocity = randomDir * speed;

            stuckTimer = 0;
        }

        lastPosition = transform.position;
    }
}