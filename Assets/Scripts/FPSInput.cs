using UnityEngine;

public class FPSInput : MonoBehaviour
{
    public float speed = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Determine XZ movement
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, 1);
        movement *= Time.deltaTime * speed;
        movement = transform.TransformDirection(movement);
        GetComponent<CharacterController>().Move(movement);
    }
}
