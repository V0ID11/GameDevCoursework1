using UnityEngine;
using UnityEngine.InputSystem;

public class Bounce : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 velocity;
    public int totalVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float angle = Mathf.Atan(Mathf.Abs(rb.linearVelocity.y) / Mathf.Abs(rb.linearVelocity.x));
        
        int upDirection = (rb.linearVelocity.y > 0) ? 1 : -1;
        int sideDirection = (rb.linearVelocity.x > 0) ? 1 : -1;
        
        rb.linearVelocity = new Vector2(Mathf.Cos(angle) * totalVelocity * sideDirection, Mathf.Sin(angle) * totalVelocity * upDirection);
    }
}
