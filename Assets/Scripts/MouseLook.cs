using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{

    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHoriz = 9.0f;
    public float sensitivityVert = 9.0f;
    public float minVert = -45.0f;
    public float maxVert = 45.0f;
    private float rotationX = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            // Horizontal rotation here
            float deltaHoriz = Input.GetAxis("Mouse X") * sensitivityHoriz;
            transform.Rotate(Vector3.up * deltaHoriz);
        } else if (axes == RotationAxes.MouseY)
        {
            // Vertical rotation here
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
            transform.localEulerAngles = new Vector3 (rotationX, 0, 0);
        } else
        {
            // Both horizontal and vertical rotation here
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
            float deltaHoriz = Input.GetAxis("Mouse X") * sensitivityHoriz;
            float rotationY = transform.localEulerAngles.y + deltaHoriz;
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
    }
}
