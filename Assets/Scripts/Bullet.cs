using UnityEngine;

public class Bullet : MonoBehaviour
{


    public float speed = 10f;
    public float lifeTime = 2f;

    private Vector3 direction;

    public void setDirection(Vector3 dir)
    {
        direction = dir.normalized;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }   
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
