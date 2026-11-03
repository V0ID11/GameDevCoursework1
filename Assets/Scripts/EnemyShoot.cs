using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyShoot : MonoBehaviour
{
    InputAction mouseAction;    
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

    // Update is called once per frame
    void Update()
    {
        if (player != null){
        if (timeout <= 0) {
            timeout = cooldown * settingsManager.settings.ArrowShotCooldown;

            Vector3 playerPos = player.transform.position;

            Vector2 direction = (playerPos - transform.position).normalized;

            GameObject newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

            // Safely check for Homing Arrow (Will skip for fireballs)
            if (newArrow.TryGetComponent<HomingEnemyArrow>(out HomingEnemyArrow homing))
            {
                homing.speed = arrowSpeed * settingsManager.settings.ArrowSpeed;
            }

            // Both Arrow and Fireball have this, so it will run for both
            if (newArrow.TryGetComponent<ProjectileMovement>(out ProjectileMovement projMove))
            {
                projMove.velocity = direction * arrowSpeed * settingsManager.settings.ArrowSpeed;
            }

            newArrow.transform.Rotate(0, 0, -45);

            // Both Arrow and Fireball have this
            if (newArrow.TryGetComponent<Weapon>(out Weapon weapon))
            {
                weapon.SetWeaponDamage(settingsManager.settings.ArrowDamage);
            }

            // Safely check for Arrow Collision (Will skip for fireballs)
            if (newArrow.TryGetComponent<ArrowCollision>(out ArrowCollision arrowCol))
            {
                arrowCol.pierce = settingsManager.settings.ArrowPierce;
                arrowCol.ricochet = settingsManager.settings.ArrowRicochet;
            }

            AudioManager.instance.PlaySFX(SoundType.Arrow);

        }
        // Debug.Log(timeout);
        if (timeout > 0){timeout = timeout - Time.deltaTime;}
        
    }}
}