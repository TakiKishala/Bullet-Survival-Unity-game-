using UnityEngine;

public class Shooter : MonoBehaviour
{
    public ObjectPool bulletPool;
    public ObjectPool smokePool;
    public Transform firePoint;

    public CameraShake cameraShake;

    public Animator armAnimation;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bulletPool.Get(firePoint.position, firePoint.rotation);

            armAnimation.SetTrigger("isFired");

            SFXManager.Instance.PlaySFX(SFXManager.Instance.playerShot);

            smokePool.Get(firePoint.position, firePoint.rotation);

            StartCoroutine(cameraShake.Shake(0.1f, 0.15f));
        }
    }
}
