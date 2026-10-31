using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class OrchSpawner : MonoBehaviour
{
    public GameObject[] level1Enemies;
    public GameObject[] level2Enemies;
    public GameObject[] level3Enemies;
    // public GameObject basicBoss;
    // public GameObject meleeBoss;
    // public GameObject rangedBoss;

    public float[] level1rates;
    public float[] level2rates;
    public float[] level3rates;
    public float[] endlessrates;
    public bool endless = false;
    private int currentlevel = 1;
    public int max_enemies;
    float sum_of_rates = 0;
    
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;

    public float round_length;
    public float round_timer;
    public bool boss = false;
    public float spawn_cooldown;
    public float spawn_timer = 0;
    public GameObject warningPrefab;

    public List<GameObject> spawned_enemies; 

    void Start() {
        round_timer = round_length;

        spawned_enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {   
        Debug.Log(currentlevel);
        if (round_timer <= 0){
            
            /*
            if (!boss)
            {
                currentlevel++;
                //spawn boss
                boss = true;
                // boss will reset our timer on death
            }
            */
            if (currentlevel == 4)
            {
                endless = true;
            }
            else
            {
                currentlevel++;
            }
            round_timer = round_length;

        } else
        {
            spawn_timer -= Time.deltaTime;
            round_timer -= Time.deltaTime;
        }

        spawned_enemies.RemoveAll(enemy => enemy == null);

        if (spawned_enemies.Count == 0) {
            
            int number_to_spawn = Random.Range(spawned_enemies.Count + 1, max_enemies) - spawned_enemies.Count;
           
            spawn_enemies(number_to_spawn);
            
        } else if (spawn_timer <= 0 && spawned_enemies.Count < max_enemies) {
            
            int number_to_spawn = Random.Range(spawned_enemies.Count + 1, max_enemies) - spawned_enemies.Count;
            spawn_enemies(number_to_spawn);
        }
    }


    void spawn_enemies(int number) {
        
        float[] currentRates;
        GameObject[] currentEnemies;

        if (currentlevel == 1){currentRates = level1rates; currentEnemies = level1Enemies;}
        else if (currentlevel == 2){currentRates = level2rates; currentEnemies = level2Enemies;}
        else if (currentlevel == 3){currentRates = level3rates; currentEnemies = level3Enemies;}
        else {currentRates = endlessrates; currentEnemies = level3Enemies;}

        sum_of_rates = 0;
        foreach (float rate in currentRates){
            sum_of_rates += rate;
        }

         for (int i = 0; i < number; i++){
            float spawn_chance = Random.Range(0f, sum_of_rates);
            
            float track = 0;
            for (int j = 0; j < currentRates.Length; j++){
                track += currentRates[j];
                if (spawn_chance < track) {
                    Vector2 spawn_pos = new Vector2(Random.Range(left.transform.position.x + 0.5f, right.transform.position.x - 0.5f), 
                                                    Random.Range(bottom.transform.position.y +0.5f, top.transform.position.y - 0.5f));
                    
                    
                    GameObject warningObj = Instantiate(warningPrefab, spawn_pos, Quaternion.identity);

                    warningObj.GetComponent<Warning>().Setup(currentEnemies[j], this);

                    spawned_enemies.Add(warningObj);

                    break;
                }
            }
                
        }
        spawn_timer = spawn_cooldown;
    }

}