using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float scale = 1f;
    public Health player;

    void Start()
    {
        if (player == null)
        {
            player = transform.parent.GetComponent<Health>();
        }

        if (player != null)
        {
            player.OnTakeDamage += updateHealthBar;
        }
        else
        {
            Debug.LogError("Health component not found for HealthBar!");
        }
    }
    
    public void updateHealthBar(int current_health, int totalHealth)
    {
        float healthPercentage = Mathf.Clamp01((float)current_health / totalHealth);
        transform.localScale = new Vector3(scale * healthPercentage, 0.07f, 1f);
    }

    void OnDestroy()
    {
        if (player != null)
        {
            player.OnTakeDamage -= updateHealthBar;
        }
    }
}