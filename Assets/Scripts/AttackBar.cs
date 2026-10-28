using UnityEngine;
using UnityEngine.UI;

public class AttackBar : MonoBehaviour
{
    public Image barFill;
    
    public SwapPlayer swapManager;
    public SwordAttack swordAbility;
    public Shoot bowAbility;

    void Start()
    {
        if (swapManager == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                swapManager = playerObj.GetComponent<SwapPlayer>();

                swordAbility = playerObj.GetComponentInChildren<SwordAttack>(true);
                bowAbility = playerObj.GetComponentInChildren<Shoot>(true);
            }
        }
    }

    void Update()
    {
        if (swapManager == null || barFill == null) return;

        float activeCooldown = 0.0001f;
        float activeTimeout = 0f;

        if (swapManager.swordEnabled && swordAbility != null)
        {
            activeCooldown = Mathf.Max(swordAbility.cooldown, 0.0001f);
            activeTimeout = swordAbility.timeout;
        }
        else if (!swapManager.swordEnabled && bowAbility != null)
        {
            activeCooldown = Mathf.Max(bowAbility.cooldown, 0.0001f);
            activeTimeout = bowAbility.timeout;
        }

        float cooldownPercentage = activeTimeout / activeCooldown;
        float targetFill = 1f - cooldownPercentage;
        
        if (Mathf.Abs(barFill.fillAmount - targetFill) > 0.001f)
        {
            barFill.fillAmount = targetFill;
        }
    }
}