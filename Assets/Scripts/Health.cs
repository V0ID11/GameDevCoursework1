using UnityEngine;

public class Health : MonoBehaviour
{
    public int totalHealth;
    public int current_health;
    public void SubtractHealth(int damage){
        current_health -= damage; 
        Debug.Log(current_health);
        if (current_health <= 0){
            Destroy(gameObject);
        }
    }
}
