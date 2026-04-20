using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{

    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        transform.position = mousePos;
    }
}