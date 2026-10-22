using UnityEngine;

public class Health : MonoBehaviour
{
    public int totalHealth;
    public int current_health;

    public void SubtractHealth(int damage){
        health -= damage; 
        Debug.Log(health);
        if (health <= 0){
            Destroy(gameObject);
        }
    }
}
