
using UnityEngine;

public class IsoPlayerController : MonoBehaviour
{
    public static IsoPlayerController Instance;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Vector2 isoMovement;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontal, vertical).normalized;

        // Correct isometric conversion
        isoMovement = new Vector2(
            movement.x + movement.y,
            movement.y - movement.x
        ).normalized;

        // Update animator parameters
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

        //animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        //AdjustPlayerFacingDirection();
        // Move the player using Rigidbody2D for better physics interactions
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

    
        if (mousePos.x < playerScreenPoint.x)
        {
            // Face left
            spriteRenderer.flipX = true;
            
        }
        else
        {
            // Face right
            spriteRenderer.flipX = false;
            
        }
    }
}