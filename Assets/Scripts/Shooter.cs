using UnityEngine;

public class Shooter : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject smokeFX;
    public Transform firePoint;

    public CameraShake cameraShake;


    public Animator armAnimation;
  

    
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            //bullet.GetComponent<Bullet>().setDirection(firePoint.right);

            armAnimation.SetTrigger("isFired");

            SFXManager.Instance.PlaySFX(SFXManager.Instance.playerShot);


            GameObject smoke = Instantiate(smokeFX, firePoint.position, firePoint.rotation);

            StartCoroutine(cameraShake.Shake(0.1f, 0.15f));


        }
    }
}
