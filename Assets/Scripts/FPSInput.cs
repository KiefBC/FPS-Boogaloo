using UnityEngine;

public class FPSInput : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = -9.8f;
    private int maxJumps = 2;
    private int jumpCount = 0;
    private float verticalSpeed = 0;
    [SerializeField] private CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Determine XZ movement
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, 1);
        movement *= speed;

        // Handle jumping
        if (charController.isGrounded)
        {
            verticalSpeed = 0;
            jumpCount = 0; // Reset jump count when grounded
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            verticalSpeed = jumpSpeed;
            jumpCount++;
        }

        // Apply gravity
        verticalSpeed += gravity * Time.deltaTime;
        movement.y = verticalSpeed;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
    }
}