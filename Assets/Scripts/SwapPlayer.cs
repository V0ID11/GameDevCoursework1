using UnityEngine;
using UnityEngine.InputSystem;

public class SwapPlayer : MonoBehaviour
{
    public GameObject swordPivot;
    public GameObject bowPivot;
    public GameObject aimLine;
    public bool swordEnabled;

    InputAction SwapAction;

    public float cooldown;
    public float timeout = 0;

    SettingsManager playerStats;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwapAction = InputSystem.actions.FindAction("SwapPlayer");
        playerStats = GetComponent<SettingsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SwapAction.triggered && timeout <= 0) {
            if (swordEnabled){
                bowPivot.SetActive(true);

                if (playerStats.settings.AimLineEnabled){
                    aimLine.SetActive(true);
                    aimLine.GetComponent<AimLineController>().maxAimDistance = playerStats.settings.AimLineSize;
                }
                
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
