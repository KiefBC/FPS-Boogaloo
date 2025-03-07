using UnityEngine;

public class WanderingIguana : MonoBehaviour
{
    private float iguanaSpeed = 3.0f;
    private float obstacleRange = 9.0f;
    
    private Animator animator;

    private float turn = 0.0f;
    private float sphereRadius = 0.512822f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = iguanaSpeed;
    }

    private void Move(float turn, float forward)
    {
        float dampTime = 0.2f;
        if (animator != null)
        {
            animator.SetFloat("Turn", turn, dampTime, Time.deltaTime);
            animator.SetFloat("Forward", forward, dampTime, Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Determine if we are heading for an obstacle
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        
        // Test for Collision
        if (Physics.SphereCast(ray, 0.5f, out hit))
        {
            // Is there an obstacle in front of us?
            if (hit.distance < obstacleRange)
            {
                // If our turn is 0, we need to decide which way to turn
                if (Mathf.Approximately(turn, 0.0f))
                {
                    // Flip a coin to decide which way to turn
                    turn = Random.Range(0, 2) == 0 ? -0.75f : 0.75f;
                }
                
                // Turn quickly
                Move(turn, 0.1f);
            }
            else // No obstacle
            {
                float forwardSpeed = Random.Range(0.05f, 1.0f);
                turn = 0.0f;
                
                Move(turn, forwardSpeed);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Gizmo Color
        Vector3 rangeTest = transform.position + transform.forward * obstacleRange; // Range Test
        Debug.DrawLine(transform.position, rangeTest); // Draw Line from Enemy to Range Test
        Gizmos.DrawWireSphere(rangeTest, sphereRadius); // Draw Sphere at Range Test
    }
}
