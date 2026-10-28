using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
    public Image healthBarFill;   
    public Image damageTrailFill; 

    public float trailShrinkSpeed = 1f; 

    public Health player;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<Health>();
            }
        }

        if (player != null)
        {
            player.OnTakeDamage += UpdateHealthBar;
            
            UpdateHealthBar(player.current_health, player.totalHealth);
            
            if (damageTrailFill != null)
                damageTrailFill.fillAmount = 1f;
        }
        else
        {
            Debug.LogError("Health component not found for HealthBar!");
        }
    }
    
    public void UpdateHealthBar(int current_health, int totalHealth)
    {
        if (totalHealth <= 0) 
        {
            totalHealth = 1; 
        }

        float healthPercentage = (float)current_health / totalHealth;
        
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = healthPercentage;
        }
        
        if (damageTrailFill != null && damageTrailFill.fillAmount < healthPercentage)
        {
            damageTrailFill.fillAmount = healthPercentage;
        }
    }

    void Update()
    {
        if (damageTrailFill != null && healthBarFill != null)
        {
            if (damageTrailFill.fillAmount > healthBarFill.fillAmount)
            {

                damageTrailFill.fillAmount = Mathf.MoveTowards(
                    damageTrailFill.fillAmount, 
                    healthBarFill.fillAmount, 
                    (trailShrinkSpeed / 3) * Time.unscaledDeltaTime
                );
            }
        }
    }

    void OnDestroy()
    {
        if (player != null)
        {
            player.OnTakeDamage -= UpdateHealthBar;
        }
    }
}