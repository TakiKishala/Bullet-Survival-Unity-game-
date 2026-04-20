using UnityEngine;

public class SmokeFX : MonoBehaviour
{
    public float lifeTime = 0.75f; // fallback destroy time

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        // Option 1: destroy using animation length (best)
        if (anim != null)
        {
            float clipLength = anim.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, clipLength);
        }
        else
        {
            // fallback if no animator
            Destroy(gameObject, lifeTime);
        }
    }
}