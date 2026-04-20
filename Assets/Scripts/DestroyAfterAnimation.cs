using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    public float lifeTime = 0.9f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
