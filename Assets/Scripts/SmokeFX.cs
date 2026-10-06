using UnityEngine;

public class SmokeFX : MonoBehaviour
{
    public float lifeTime = 1.6f;

    private float remainingLife;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        remainingLife = lifeTime;

        if (anim != null)
        {
            anim.Rebind();
            anim.Update(0f);
        }
    }

    private void Update()
    {
        remainingLife -= Time.deltaTime;
        if (remainingLife <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
