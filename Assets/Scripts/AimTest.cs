using UnityEngine;

public class AimTest : MonoBehaviour
{
    public Transform armPivot;
    public Transform rightShoulder;
    public Transform leftShoulder;
    public Transform centerPoint;

    public SpriteRenderer bodySprite;
    public SpriteRenderer armSprite;

    void Update()
    {
        AimArm();
    }

    void AimArm()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        bodySprite.flipX = mousePos.x < centerPoint.position.x;

        if (bodySprite.flipX)
            armPivot.position = leftShoulder.position;
        else
            armPivot.position = rightShoulder.position;

        Vector3 direction = mousePos - armPivot.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        armPivot.rotation = Quaternion.Euler(0, 0, angle);

        armSprite.flipY = direction.x < 0;
    }
}
