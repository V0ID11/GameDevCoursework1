using UnityEngine;
using UnityEngine.UI;

public class SwapBar : MonoBehaviour
{
    public Image barFill;
    public SwapPlayer swapAbility;

    void Start()
    {
        if (swapAbility == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                swapAbility = playerObj.GetComponent<SwapPlayer>();
            }
        }
    }

    void Update()
    {
        if (swapAbility != null && barFill != null)
        {
            float maxCooldown = Mathf.Max(swapAbility.cooldown, 0.0001f);
            float cooldownPercentage = swapAbility.timeout / maxCooldown;
            float targetFill = 1f - cooldownPercentage;
            
            if (Mathf.Abs(barFill.fillAmount - targetFill) > 0.001f)
            {
                barFill.fillAmount = targetFill;
            }
        }
    }
}