using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player; // The target the camera will follow
    public float smoothSpeed = 5f; // The speed of the camera's movement
    public Vector3 offset; // The offset from the target's position
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void LateUpdate()
    {
       Vector3 targetPosition = player.position + offset; // Calculate the desired position of the camera 

        targetPosition.z = transform.position.z; // Keep the camera's z position unchanged

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime); // Smoothly interpolate between the current position and the target position
       transform.position = smoothedPosition; // Update the camera's position
    }
}
