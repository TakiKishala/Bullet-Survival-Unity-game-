using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private bool hasHit;

    public float remainingLifeTime;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        remainingLifeTime = lifeTime;
        hasHit = false;
        rb.angularVelocity = 0f;
        rb.linearVelocity = (Vector2)transform.right * speed;
    }

    private void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
    }

    void Update()
    {

        remainingLifeTime -= Time.deltaTime;

        if (remainingLifeTime <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
    public bool TryHit()
    {
        if (hasHit)
        {
            return false;
        }
        hasHit = true;
        return true;
    }
}
