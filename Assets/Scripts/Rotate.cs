using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float RotationSpeed;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.angularVelocity = 0;
        transform.Rotate(0.0f,0.0f, RotationSpeed * GameSettings.GlobalSpeedMultplier, Space.Self);
        
    }
}
