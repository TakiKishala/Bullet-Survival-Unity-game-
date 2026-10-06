using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public Animator animator;
    private bool isDead = false;

    public GameObject bloodFXprefab;

    public GameObject enemyHands;
    public GameObject enemyFootShadow;

    public EnemySpawnManager enemySpawnManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if(bullet == null || !bullet.TryHit())
            {
                return;
            }

            ContactPoint2D contact = collision.contacts[0];
            GameObject bloodFX = Instantiate(bloodFXprefab, contact.point, Quaternion.identity);
            Destroy(bloodFX, 1f);
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        if (enemySpawnManager != null)
        {
            enemySpawnManager.EnemyDied();
        }
        Destroy(enemyHands);
        Destroy(enemyFootShadow);

        ScoreManager.instance.AddScore(1);

        SFXManager.Instance.PlayRandom(SFXManager.Instance.hitEnemy, SFXManager.Instance.enemyVolume);
        SFXManager.Instance.PlaySFX(SFXManager.Instance.enemyImpact, SFXManager.Instance.impactVolume);

        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;

        animator.SetTrigger("isShot");

        Destroy(gameObject, 1f);
    }
}
