using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Image barFill;
    public ChangeDirection dashAbility;

    void Start()
    {
        if (dashAbility == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                dashAbility = playerObj.GetComponent<ChangeDirection>();
            }
        }
    }

    void Update()
    {
        if (dashAbility != null && barFill != null)
        {
            float maxCooldown = Mathf.Max(dashAbility.cooldown, 0.0001f);
            float cooldownPercentage = dashAbility.timeout / maxCooldown;
            float targetFill = 1f - cooldownPercentage;
            
            if (Mathf.Abs(barFill.fillAmount - targetFill) > 0.001f)
            {
                barFill.fillAmount = targetFill;
            }
        }
    }
}