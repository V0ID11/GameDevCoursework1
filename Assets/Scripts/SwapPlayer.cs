using UnityEngine;
using UnityEngine.InputSystem;

public class SwapPlayer : MonoBehaviour
{
    public GameObject swordPivot;
    public GameObject bowPivot;
    public bool swordEnabled;

    InputAction SwapAction;

    public float cooldown;
    float timeout = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwapAction = InputSystem.actions.FindAction("SwapPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        if (SwapAction.triggered && timeout <= 0) {
            if (swordEnabled){
                bowPivot.SetActive(true);
                swordPivot.SetActive(false);
            } else {
                bowPivot.SetActive(false);
                swordPivot.SetActive(true);
            }
            swordEnabled = !swordEnabled;
            timeout = cooldown;
        }
        if (timeout > 0){
            timeout -= Time.deltaTime;
        }
    }
}
