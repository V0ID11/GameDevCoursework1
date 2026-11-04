using UnityEngine;

public class Warning : MonoBehaviour
{
    public float warningDuration = 1f; // How long the warning stays on screen
    
    private GameObject plannedEnemyPrefab;
    private OrchSpawner parentSpawner;
    private float timer;
    private int level;

    public void Setup(GameObject enemyToSpawn, int currentlevel=-1, OrchSpawner spawner = null)
    {
        plannedEnemyPrefab = enemyToSpawn;
        if (spawner != null)
        {
            parentSpawner = spawner;
        }
        timer = warningDuration;
        if (currentlevel != -1){
        level = currentlevel; }
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
            SettingsManager ScalableStats = realEnemy.GetComponent<SettingsManager>();

            ScalableStats.settings.total_health = ScalableStats.settings.total_health * (1 + (1/(level*3)));
            ScalableStats.settings.current_health = ScalableStats.settings.current_health * (1 + (1/(level*3))); 
            ScalableStats.settings.SwordDamage = ScalableStats.settings.SwordDamage * (1 + (1/(level*3)));
            ScalableStats.settings.ArrowDamage = ScalableStats.settings.ArrowDamage * (1 + (1/(level*3)));
            ScalableStats.settings.ExtraDaggerDamage = ScalableStats.settings.ExtraDaggerDamage * (1 + (1/(level*3)));


            
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
