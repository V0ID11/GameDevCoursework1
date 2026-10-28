using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeDirection : MonoBehaviour
{
    InputAction mouseAction;    
    public float timeout;
    public float cooldown;
    Bounce bounceScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseAction = InputSystem.actions.FindAction("Attack");
        bounceScript = GetComponent<Bounce>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseAction.triggered && (timeout <= 0)) {
            Vector2 screenPos = Pointer.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            bounceScript.updateDirection(worldPos);
            timeout = cooldown;
        }
        // Debug.Log(timeout);
        if (timeout > 0){timeout = timeout - Time.deltaTime;}
        
    }
}
