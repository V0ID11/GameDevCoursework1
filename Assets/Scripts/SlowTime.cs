using UnityEngine;
using UnityEngine.InputSystem;


public class SlowTime : MonoBehaviour
{
    InputAction SlowAction;
    public float cooldown;
    public float timeout = 0;
    float slowTiming=0;
    bool slowed = false;
    SettingsManager playerStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SlowAction = InputSystem.actions.FindAction("SlowTime");
        playerStats = GetComponent<SettingsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SlowAction.triggered && timeout <= 0) {
            Time.timeScale  = 0.5f;
            timeout = cooldown * playerStats.settings.FocusAbilityCooldown;
            slowTiming = 1.5f;
            slowed = true;
            
        }
        ;
        if (timeout > 0 && slowTiming <= 0) {
            if (slowed){
               
                Time.timeScale = 1.0f;
                slowed = false;
            }
            timeout -= Time.deltaTime;
        } else if (slowTiming > 0) {
            slowTiming -= Time.deltaTime;
        }
        
    }
}
