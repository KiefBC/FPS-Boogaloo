using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 6f;
    private float toNewtons = 100;
    [SerializeField] private Rigidbody rb;
    // Update is called once per frame
    void FixedUpdate()
    {
        // OPTION 1: determine forward movement in world coordinates, then convert
        // it to movement in terms of the laser�s forward direction.
        Vector3 movement = Vector3.forward * Time.deltaTime * speed * toNewtons;
        // convert between forward in world coords to forward in local coords
        movement = transform.TransformDirection(movement);
        // OPTION 2: a bit simpler!
        // no conversion required since we�re using the laser�s local forward vector.
        // Vector3 movement = transform.forward * Time.deltaTime * speed * toNewtons;
        rb.linearVelocity = movement;
    }
    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            player.Hit();
        }
        else
        {
            Debug.Log("Laser did not connect!");
        }

        Destroy(this.gameObject);
    }
}