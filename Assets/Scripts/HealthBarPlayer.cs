using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
    public Image healthBarFill; 
    public Health player;

    void Start()
    {

        if (player != null)
        {
            player.OnTakeDamage += UpdateHealthBar;
            
            UpdateHealthBar(player.current_health, player.totalHealth);
        }
        else
        {
            Debug.LogError("Health component not found for HealthBar!");
        }
    }
    
    public void UpdateHealthBar(int current_health, int totalHealth)
    {
        float healthPercentage = (float)current_health / totalHealth;
        
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = healthPercentage;
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
