using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public AudioSource audioSource;

    [Header("Volume Controls")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float gunVolume = 1f;
    [Range(0f, 1f)] public float enemyVolume = 1f;
    [Range(0f, 1f)] public float impactVolume = 0.1f;


    [Header("Gun")]
    public AudioClip[] enemyGunShots;

    [Header("Grunts")]
    public AudioClip[] hitEnemy;
    public AudioClip hitWall;

    [Header("Enemy Impact")]
    public AudioClip enemyImpact;

    [Header("Player")]
    public AudioClip playerHit;
    public AudioClip playerShot;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f); // variation
        audioSource.PlayOneShot(clip, volume);
    }
    public void PlayRandom(AudioClip[] clips, float volume = 1f)
    {
        if (clips.Length == 0) return;

        int index = Random.Range(0, clips.Length);

        float finalVolume = volume * masterVolume;

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clips[index], finalVolume);
    }
}