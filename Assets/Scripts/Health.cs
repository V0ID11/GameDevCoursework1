using System; // Required for Action
using UnityEngine;

public class Health : MonoBehaviour
{
    public int totalHealth;
    public int current_health;
    float i_frames=0.5f;
    float i_frames_count=0;
    

    public Action<int, int> OnTakeDamage;

    public void Start()
    {
        current_health = totalHealth;
    }

    void FixedUpdate()
    {
        if (i_frames_count >= 0){
            i_frames_count -= Time.deltaTime; 
        }
    }

    public void SubtractHealth(int damage)
    {
        if (i_frames_count <= 0) {
            current_health -= damage; 
            Debug.Log(current_health);
            

            OnTakeDamage?.Invoke(current_health, totalHealth);
            
            
            Time.timeScale = 0f;
            float endTime = Time.realtimeSinceStartup + 0.1f;
            while (Time.realtimeSinceStartup <  endTime){
                continue;
            }
            Time.timeScale = 1f;
            i_frames_count = i_frames;
        }
        
        if (current_health <= 0)
        {   
            if (gameObject.tag == "Player") {
                Destroy(gameObject);
            
                Time.timeScale  = 0f;
            } else {
                Destroy(gameObject);
            }
            
        }
    }
}
