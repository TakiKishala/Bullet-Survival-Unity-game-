using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject AmbiantSound; // Reference to the GameObject that contains the AudioManager script
    public Animator animator; // Reference to the Animator component
    private bool PlayerIsDead = false; // Flag to check if the enemy is already dead

    public GameObject playerHands; // Reference to the player's hands GameObject

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PlayerIsDead && collision.gameObject.CompareTag("EnemyBullet"))
        {
            Die();
        }
    }
    void Die()
    {
        PlayerIsDead = true; // Set the flag to prevent multiple triggers
        playerHands.SetActive(false); // Deactivate the player's hands GameObject to hide them during the death animation

        Time.timeScale = 0.3f; // Pause the game by setting time scale to 0
        GetComponent<IsoPlayerController>().enabled = false; // Disable the player's movement script to prevent further input
        GetComponentInParent<Shooter>().enabled = false; // Disable the player's shooting script to prevent further shooting
        GetComponentInParent<AimTest>().enabled = false; // Disable the player's aiming script to prevent further aiming

        Invoke(nameof(TriggerGameOver), 1f); // Schedule the time scale to reset after a short delay

        AmbiantSound.SetActive(false); 



        SFXManager.Instance.PlaySFX(SFXManager.Instance.playerHit); // Play the enemy death sound effect
        SFXManager.Instance.PlaySFX(SFXManager.Instance.enemyImpact, SFXManager.Instance.impactVolume); // Play the enemy death sound effect

        //GetComponent<EnemyAI>().enabled = false; // Disable the enemy's AI script
        //GetComponent<Collider2D>().enabled = false; // Disable the enemy's collider to prevent further interactions

        animator.SetTrigger("playerIsShot"); 
        animator.SetBool("playerIsDead", true); 

        //Destroy(gameObject, 1f); // Destroy the enemy after a delay to allow the animation to play
    }
    void TriggerGameOver()
    {
        Time.timeScale = 0f; // Pause the game by setting time scale to 0
        Object.FindAnyObjectByType<GameManager>().ShowGameOver(); // Call the ShowGameOver method in the GameManager to display the game over screen
    }
}
