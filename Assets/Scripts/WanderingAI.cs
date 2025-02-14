using UnityEngine;

public enum EnemyStates { alive, dead };

public class WanderingAI : MonoBehaviour
{
    private float enemySpeed = 3.0f;
    private float obstacleRange = 5.0f;
    private float sphereRadius = 0.512822f;

    private EnemyStates state;

    [SerializeField] private GameObject laserbeamPrefab;
    private GameObject laserbeam;
    public float fireRate = 2.0f;
    private float nextFire = 0.0f;

    void Awake()
    {
        state = EnemyStates.alive;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyStates.alive)
        {
            Vector3 movement = enemySpeed * Time.deltaTime * Vector3.forward;
            transform.Translate(movement);
            Ray ray = new(transform.position, transform.forward);

            // Spherecast and check for obstacles
            RaycastHit hit;
            if (Physics.SphereCast(ray, sphereRadius, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (laserbeam == null && Time.time > nextFire)
                    {
                        Debug.Log("Enemy Fire!");
                        nextFire = Time.time + fireRate;
                        laserbeam = Instantiate(laserbeamPrefab) as GameObject;
                        laserbeam.transform.position = transform.TransformPoint(0, 1.5f, 1.5f);
                        laserbeam.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    // Hit Obstacle, TURN!
                    float turnAngle = Random.Range(-110, 110);
                    transform.Rotate(Vector3.up * turnAngle);
                }
            }
        } 
        else
        {
            // Do nothing
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Gizmo Color
        Vector3 rangeTest = transform.position + transform.forward * obstacleRange; // Range Test
        Debug.DrawLine(transform.position, rangeTest); // Draw Line from Enemy to Range Test
        Gizmos.DrawWireSphere(rangeTest, sphereRadius); // Draw Sphere at Range Test
    }

    public void ChangeState(EnemyStates newState)
    {
        this.state = newState;
    }
}
