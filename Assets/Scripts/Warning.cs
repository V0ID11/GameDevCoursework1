using UnityEngine;

public class Warning : MonoBehaviour
{
    public float warningDuration = 1f; // How long the warning stays on screen
    
    private GameObject plannedEnemyPrefab;
    private OrchSpawner parentSpawner;
    private float timer;
    private int level=1;
    int scale = 1;

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

            
            if (level % 3 == 0){
                scale += 1;
            }

            ScalableStats.settings.total_health = (int) ScalableStats.settings.total_health * (1 + ((scale) / 10));
            ScalableStats.settings.current_health = (int) ScalableStats.settings.current_health * (1 + ((scale) / 10)); 
            ScalableStats.settings.SwordDamage = (int) ScalableStats.settings.SwordDamage * (1 + ((scale) / 10));
            ScalableStats.settings.ArrowDamage = (int) ScalableStats.settings.ArrowDamage * (1 + ((scale) / 10));
            ScalableStats.settings.ExtraDaggerDamage = (int) ScalableStats.settings.ExtraDaggerDamage * (1 + ((scale) / 10));


            
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
