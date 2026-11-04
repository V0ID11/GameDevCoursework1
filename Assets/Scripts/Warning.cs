using UnityEngine;

public class Warning : MonoBehaviour
{
    public float warningDuration = 1f; // How long the warning stays on screen
    
    private GameObject plannedEnemyPrefab;
    private OrchSpawner parentSpawner;
    private float timer;

    public void Setup(GameObject enemyToSpawn, OrchSpawner spawner = null)
    {
        plannedEnemyPrefab = enemyToSpawn;
        if (spawner != null)
        {
            parentSpawner = spawner;
        }
        timer = warningDuration;
    }

    void Update()
{
    timer -= Time.deltaTime;
    
    if (timer <= 0)
    {
        // Safety check to prevent crashing if the reference is null or destroyed
        if (plannedEnemyPrefab != null)
        {
            GameObject realEnemy = Instantiate(plannedEnemyPrefab, transform.position, Quaternion.identity);
            
            if (parentSpawner != null)
            {
                parentSpawner.spawned_enemies.Add(realEnemy);
            }
        }
        else
        {
            Debug.LogError("plannedEnemyPrefab was null or destroyed when the warning timer expired!", this);
        }

        Destroy(gameObject);
    }
}
}
