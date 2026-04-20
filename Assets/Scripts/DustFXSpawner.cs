using UnityEngine;

public class DustFXSpawner : MonoBehaviour
{
    public GameObject dustPrefab;
    public Transform feetPoint;
    public GameObject testSprite;

    public float spawnRate = 0.1f;
    private float timer;

    private Vector2 lastMoveDir;

    void Update()
    {
        Vector2 moveDir = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // Only update direction if moving
        if (moveDir != Vector2.zero)
        {
            lastMoveDir = moveDir;

            timer += Time.deltaTime;

            if (timer >= spawnRate)
            {
                SpawnDust(moveDir);
                timer = 0f;
            }
        }
    }

    void SpawnDust(Vector2 moveDir)
    {
        GameObject dust = Instantiate(dustPrefab, feetPoint.position, Quaternion.identity);

        // Rotate opposite of movement
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        dust.transform.rotation = Quaternion.Euler(0, 0, angle + 180f);
    }
}