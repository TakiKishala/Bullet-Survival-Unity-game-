using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject AmbiantSound;
    public Animator animator;
    private bool PlayerIsDead = false;
    public bool playerIsInvisible = false;

    public GameObject playerHands;

    public void Start()
    {
        playerIsInvisible = GameManager.playerInvisible;
    }

    public void SetPlayerInvisible(bool isOptionOn)
    {
        playerIsInvisible = isOptionOn;
        GameManager.playerInvisible = isOptionOn;
        Debug.Log("Player invisibility set to: " + playerIsInvisible);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PlayerIsDead && collision.gameObject.CompareTag("EnemyBullet") && !playerIsInvisible)
        {
            Die();
        }
    }

    public void Die()
    {
        PlayerIsDead = true;
        playerHands.SetActive(false);

        Object.FindAnyObjectByType<GameManager>().BeginDying();
        GetComponent<IsoPlayerController>().enabled = false;
        GetComponentInParent<Shooter>().enabled = false;
        GetComponentInParent<AimTest>().enabled = false;

        // This delay uses scaled time, so death slow motion extends it in real time.
        Invoke(nameof(TriggerGameOver), 1f);

        AmbiantSound.SetActive(false);

        SFXManager.Instance.PlaySFX(SFXManager.Instance.playerHit);
        SFXManager.Instance.PlaySFX(SFXManager.Instance.enemyImpact, SFXManager.Instance.impactVolume);

        animator.SetTrigger("playerIsShot");
        animator.SetBool("playerIsDead", true);
    }

    void TriggerGameOver()
    {
        Time.timeScale = 0f;
        Object.FindAnyObjectByType<GameManager>().ShowGameOver();
    }
}
