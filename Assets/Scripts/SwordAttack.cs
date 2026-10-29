using UnityEngine;
using UnityEngine.InputSystem;

public class SwordAttack : MonoBehaviour
{
    InputAction swingAction;
    public float cooldown;
    public float timeout = 0;
    float swingTiming=0;
    public float duration;
    bool swinging = false;
    Rotate RotationScript;
    public float speedScaling;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        swingAction = InputSystem.actions.FindAction("Shoot");
        RotationScript = gameObject.GetComponent<Rotate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (swingAction.triggered && timeout <= 0) {
            RotationScript.RotationSpeed = RotationScript.RotationSpeed * speedScaling;
            timeout = cooldown;
            swingTiming = duration;
            swinging = true;
            
        }
        ;
        if (timeout > 0 && swingTiming <= 0) {
            if (swinging){
                RotationScript.RotationSpeed = RotationScript.RotationSpeed * (1/speedScaling);
                
                swinging = false;
            }
            timeout -= Time.deltaTime;
        } else if (swingTiming > 0) {
            swingTiming -= Time.deltaTime;
        }
    }
}
