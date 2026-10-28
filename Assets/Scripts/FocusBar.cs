using UnityEngine;
using UnityEngine.UI;

public class FocusBar : MonoBehaviour
{
    public Image barFill;
    public SlowTime slowTimeAbility;

    void Start()
    {
        if (slowTimeAbility == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                slowTimeAbility = playerObj.GetComponent<SlowTime>();
            }
        }
    }

    void Update()
    {
        if (slowTimeAbility != null && barFill != null)
        {
            float maxCooldown = Mathf.Max(slowTimeAbility.cooldown, 0.0001f);
            float cooldownPercentage = slowTimeAbility.timeout / maxCooldown;
            float targetFill = 1f - cooldownPercentage;
            
            if (Mathf.Abs(barFill.fillAmount - targetFill) > 0.001f)
            {
                barFill.fillAmount = targetFill;
            }
        }
    }
}