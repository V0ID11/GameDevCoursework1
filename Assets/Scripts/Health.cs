using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 10;

    public void SubtractHealth(int damage){
        health -= damage; 
        Debug.Log(health);
        if (health <= 0){
            Destroy(gameObject);
        }
    }
}
