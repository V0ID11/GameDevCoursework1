using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeDirection : MonoBehaviour
{
    InputAction mouseAction;    
    public float timeout;
    public float cooldown;
    Bounce bounceScript;

    SettingsManager playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GetComponent<SettingsManager>();
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
            timeout = cooldown * playerStats.settings.DashAbilityCooldown;
        }
        // Debug.Log(timeout);
        if (timeout > 0){timeout = timeout - Time.deltaTime;}
        
    }
}
