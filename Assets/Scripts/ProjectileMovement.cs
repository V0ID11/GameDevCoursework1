using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = velocity;
    }
}
