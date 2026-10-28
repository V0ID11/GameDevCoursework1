using System; 
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int totalHealth;
    public int current_health;
    float i_frames = 0.5f;
    float i_frames_count = 0;

    public Action<int, int> OnTakeDamage;

    public void Awake()
    {
        current_health = totalHealth;
    }

    void FixedUpdate()
    {
        if (i_frames_count >= 0)
        {
            i_frames_count -= Time.deltaTime; 
        }
    }

    public void SubtractHealth(int damage)
    {
        if (i_frames_count <= 0) 
        {
            current_health -= damage; 

            OnTakeDamage?.Invoke(current_health, totalHealth);
            
            if (current_health > 0)
            {
                StartCoroutine(HitStopRoutine(0.1f));
            }
            
            i_frames_count = i_frames;
        }
        
        if (current_health <= 0)
        {   
            if (gameObject.CompareTag("Player")) 
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            
            Destroy(gameObject);
        }
    }

    IEnumerator HitStopRoutine(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration); 
        Time.timeScale = 1f;
    }
}