using System; // Required for Action
using UnityEngine;

public class Health : MonoBehaviour
{
    public int totalHealth;
    public int current_health;
    

    public Action<int, int> OnTakeDamage;

    public void Start()
    {
        current_health = totalHealth;
    }

    public void SubtractHealth(int damage)
    {
        current_health -= damage; 
        Debug.Log(current_health);
        

        OnTakeDamage?.Invoke(current_health, totalHealth);
        
        if (current_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}