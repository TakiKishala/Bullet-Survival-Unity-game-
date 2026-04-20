using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    
    public Animator animator; // Reference to the Animator component
    private bool isDead = false; // Flag to check if the enemy is already dead

    public GameObject bloodFXprefab; // Reference to the blood effect prefab

    public GameObject enemyHands; // Reference to the enemy's hands GameObject
    public GameObject enemyFootShadow;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Bullet"))
        {
            ContactPoint2D contact = collision.contacts[0]; // Get the contact point of the collision
            GameObject bloodFX = Instantiate(bloodFXprefab, contact.point, Quaternion.identity); // Instantiate the blood effect at the contact point
            Destroy(bloodFX, 1f); // Destroy the blood effect after a short delay
            Die();
        }
    }
   void Die()
    {
        isDead = true; // Set the flag to prevent multiple triggers
        Destroy(enemyHands); // Destroy the enemy's hands GameObject
        Destroy(enemyFootShadow); // Destroy the enemy's foot shadow GameObject


        ScoreManager.instance.AddScore(100); // Add score for killing the enemy

        SFXManager.Instance.PlayRandom(SFXManager.Instance.hitEnemy, SFXManager.Instance.enemyVolume); // Play the enemy death sound effect
        SFXManager.Instance.PlaySFX(SFXManager.Instance.enemyImpact, SFXManager.Instance.impactVolume); // Play the enemy death sound effect

        GetComponent<EnemyAI>().enabled = false; // Disable the enemy's AI script
        GetComponent<Collider2D>().enabled = false; // Disable the enemy's collider to prevent further interactions

        animator.SetTrigger("isShot"); // Trigger the death animation

        Destroy(gameObject, 1f); // Destroy the enemy after a delay to allow the animation to play
    }
}
