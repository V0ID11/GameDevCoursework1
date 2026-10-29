using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyShoot : MonoBehaviour
{
    public float cooldown;
    float timeout;
    public float arrowSpeed;
    public GameObject arrowPrefab;

    GameObject player;

    SettingsManager settingsManager;

    void Start()
    {
        timeout = cooldown;
        player = GameObject.FindWithTag("Player");

         if (!CompareTag("Arrow"))
        {
            if (transform.parent != null && transform.parent != null)
            {
                settingsManager = transform.parent.GetComponent<SettingsManager>();
            }
        }
    }

    void Update()
    {
        if (player != null){
        if (timeout <= 0) {
            timeout = cooldown * settingsManager.settings.ArrowShotCooldown;

                Vector2 playerPos = player.transform.position;
                Vector2 myPos = transform.position;
                Vector2 direction = (playerPos - myPos).normalized;

                GameObject newArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                newArrow.GetComponent<ProjectileMovement>().velocity = direction * arrowSpeed;

            GameObject newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

            if (newArrow.GetComponent<HomingEnemyArrow>() != null){
                newArrow.GetComponent<HomingEnemyArrow>().speed = arrowSpeed* settingsManager.settings.ArrowSpeed;
            }

            newArrow.GetComponent<ProjectileMovement>().velocity = direction * arrowSpeed* settingsManager.settings.ArrowSpeed;

            newArrow.transform.Rotate(0, 0, -45);

            newArrow.GetComponent<Weapon>().SetWeaponDamage(settingsManager.settings.ArrowDamage);

            newArrow.GetComponent<ArrowCollision>().pierce = settingsManager.settings.ArrowPierce;
            newArrow.GetComponent<ArrowCollision>().ricochet = settingsManager.settings.ArrowRicochet;

        }
    }
}