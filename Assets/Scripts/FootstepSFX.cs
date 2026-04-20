using UnityEngine;

public class FootstepSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;

    public void PlayFootstep()
    {
        if (footstepSounds.Length == 0) return;

        int index = Random.Range(0, footstepSounds.Length);
        audioSource.PlayOneShot(footstepSounds[index]);
    }
}