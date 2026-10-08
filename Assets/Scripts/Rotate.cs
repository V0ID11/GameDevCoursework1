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
    void Update()
    {
        transform.Rotate(0.0f,0.0f, RotationSpeed, Space.Self);
        
    }
}
