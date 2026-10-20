using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float RotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f,0.0f, RotationSpeed * Time.deltaTime, Space.Self);
        
    }
}
