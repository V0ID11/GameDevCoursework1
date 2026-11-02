using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float[] rates;
    public int max_enemies;
    public float timeout;
    float sum_of_rates = 0;
    public float cooldown;

    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;

    public float round_length;
    public GameObject warningPrefab;

    public List<GameObject> spawned_enemies; 

    void Start() {
        spawned_enemies = new List<GameObject>();
        foreach (float rate in rates){
            sum_of_rates += rate;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if (round_length <= 0){
            Destroy(gameObject);
        }
        spawned_enemies.RemoveAll(enemy => enemy == null);
        if (spawned_enemies.Count == 0) {
            
            int number_to_spawn = Random.Range(spawned_enemies.Count + 1, max_enemies) - spawned_enemies.Count;
           
            spawn_enemies(number_to_spawn);
            
        } else if (cooldown <= 0 && spawned_enemies.Count < max_enemies) {
            int number_to_spawn = Random.Range(spawned_enemies.Count + 1, max_enemies) - spawned_enemies.Count;
            spawn_enemies(number_to_spawn);
           
           
        }

        cooldown -= Time.deltaTime;
        round_length -= Time.deltaTime;
    }


    void spawn_enemies(int number) {
         for (int i = 0; i < number; i++){
                float spawn_chance = Random.Range(0f, sum_of_rates);
                
                float track = 0;
                for (int j = 0; j < rates.Length; j++){
                    track += rates[j];
                    if (spawn_chance < track) {
                        Vector2 spawn_pos = new Vector2(Random.Range(left.transform.position.x + 0.5f, right.transform.position.x - 0.5f), 
                                                        Random.Range(bottom.transform.position.y +0.5f, top.transform.position.y - 0.5f));
                        
                        
                        GameObject warningObj = Instantiate(warningPrefab, spawn_pos, Quaternion.identity);

                        //warningObj.GetComponent<Warning>().Setup(enemies[j], this);

                        spawned_enemies.Add(warningObj);

                        break;
                    }
                }
                cooldown = timeout;
    }
    }

}