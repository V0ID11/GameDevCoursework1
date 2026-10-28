using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    InputAction mouseAction;    
    public float cooldown;
    public float timeout = 0;
    public float arrowSpeed;
    public GameObject arrowPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseAction = InputSystem.actions.FindAction("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseAction.triggered && (timeout <= 0)) {
            timeout = cooldown;

            
            Vector2 screenPos = Pointer.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            
            Vector2 direction = (worldPos - (Vector2)transform.position).normalized;
            

            GameObject newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

            newArrow.GetComponent<ProjectileMovement>().velocity = direction * arrowSpeed;

            newArrow.transform.Rotate(0, 0, -45);
        }
        // Debug.Log(timeout);
        if (timeout > 0){timeout = timeout - Time.deltaTime;}
        
    }
}

