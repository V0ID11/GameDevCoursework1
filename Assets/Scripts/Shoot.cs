using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    InputAction mouseAction;    
    public float cooldown;
    public float timeout = 0;
    public float arrowSpeed;
    public GameObject arrowPrefab;

    void Start()
    {
        mouseAction = InputSystem.actions.FindAction("Shoot");
    }

    void Update()
    {
        if (mouseAction.triggered && (timeout <= 0)) 
        {
            timeout = cooldown;

            Vector2 screenPos = Pointer.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            Vector2 myPos = transform.position;
            
            Vector2 direction = (worldPos - myPos).normalized;

            GameObject newArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            newArrow.GetComponent<ProjectileMovement>().velocity = direction * arrowSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            newArrow.transform.rotation = Quaternion.Euler(0, 0, angle - 45f);
        }
        
        if (timeout > 0)
        {
            timeout -= Time.deltaTime;
        }
    }
}