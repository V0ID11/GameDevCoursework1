using UnityEngine;
using System.Collections;

public class LichAbility : MonoBehaviour
{
    private Health healthScript;
    public GameObject minotaur;
    public GameObject knight;
    public GameObject warningPrefab;
    
    private GameObject top;
    private GameObject bottom;
    private GameObject left;
    private GameObject right;

    private int totalHealth;
    private bool activated = false;

    void Start()
    {
        healthScript = GetComponent<Health>();
        
        if (healthScript != null)
        {
            totalHealth = healthScript.totalHealth;
        }

        top = GameObject.Find("TopWall");       
        bottom = GameObject.Find("BottomWall"); 
        left = GameObject.Find("LeftWall");     
        right = GameObject.Find("RightWall");   
    }

    void Update()
    {
        if (healthScript == null) return;

        int currentHealth = healthScript.current_health;

        if (!activated && currentHealth < (totalHealth / 2))
        {
            AudioManager.instance.PlaySFX(SoundType.Boss);

            if (left != null && right != null && top != null && bottom != null)
            {
                Vector2 spawn_pos1 = new Vector2(
                    Random.Range(left.transform.position.x + 0.5f, right.transform.position.x - 0.5f), 
                    Random.Range(bottom.transform.position.y + 0.5f, top.transform.position.y - 0.5f)
                );
                GameObject warningObj1 = Instantiate(warningPrefab, spawn_pos1, Quaternion.identity);
                warningObj1.GetComponent<Warning>().Setup(minotaur);

                Vector2 spawn_pos2 = new Vector2(
                    Random.Range(left.transform.position.x + 0.5f, right.transform.position.x - 0.5f), 
                    Random.Range(bottom.transform.position.y + 0.5f, top.transform.position.y - 0.5f)
                );
                GameObject warningObj2 = Instantiate(warningPrefab, spawn_pos2, Quaternion.identity);
                warningObj2.GetComponent<Warning>().Setup(knight);
            }

            activated = true;
        }
    }
}