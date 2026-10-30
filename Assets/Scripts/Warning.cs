using UnityEngine;

public class Warning : MonoBehaviour
{
    public float warningDuration = 1f; // How long the warning stays on screen
    
    private GameObject plannedEnemyPrefab;
    private Spawner parentSpawner;
    private float timer;

    public void Setup(GameObject enemyToSpawn, Spawner spawner)
    {
        plannedEnemyPrefab = enemyToSpawn;
        parentSpawner = spawner;
        timer = warningDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            GameObject realEnemy = Instantiate(plannedEnemyPrefab, transform.position, Quaternion.identity);
            
            if (parentSpawner != null)
            {
                parentSpawner.spawned_enemies.Add(realEnemy);
            }

            Destroy(gameObject);
        }
    }
}
