using UnityEngine;
using UnityEngine.InputSystem;

public class RotateBow : MonoBehaviour
{
    public float RotationSpeed = 720f; 

    void Update()
    {
        Vector2 screenPos = Pointer.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        Vector2 direction = worldPos - (Vector2)transform.position;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
    }
}