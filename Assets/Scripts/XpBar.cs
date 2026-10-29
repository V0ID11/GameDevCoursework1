using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Image barFill;
    public PlayerXP player;
    
    public float fillSpeed = 0.5f; 

    private float targetFill = 0f;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerXP>();
            }
        }

        if (player != null)
        {
            player.OnXPChanged += UpdateXPBar;
            
            UpdateXPBar(player.xp, player.for_next_level);
            
            if (barFill != null)
            {
                barFill.fillAmount = targetFill; 
            }
        }
    }

    public void UpdateXPBar(int current_xp, int for_next_level)
    {
        if (for_next_level <= 0) for_next_level = 1;
        
        targetFill = (float)current_xp / for_next_level;
    }

    void Update()
    {
        if (barFill != null)
        {
            if (barFill.fillAmount > targetFill)
            {
                barFill.fillAmount = 0f;
            }

            if (Mathf.Abs(barFill.fillAmount - targetFill) > 0.001f)
            {
                barFill.fillAmount = Mathf.MoveTowards(
                    barFill.fillAmount, 
                    targetFill, 
                    fillSpeed * Time.unscaledDeltaTime
                );
            }
        }
    }

    void OnDestroy()
    {
        if (player != null)
        {
            player.OnXPChanged -= UpdateXPBar;
        }
    }
}