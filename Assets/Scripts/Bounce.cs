using UnityEngine;
using UnityEngine.InputSystem;

public class Bounce : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = velocity;
    }
}
