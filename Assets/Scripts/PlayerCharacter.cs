using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        health -= 1;
        Debug.Log("Player Hit! New Health: " + health);
        if (health <= 0)
        {
            Debug.Break();
        }
    }
}
