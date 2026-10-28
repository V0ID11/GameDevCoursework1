using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyShoot : MonoBehaviour
{
    public float cooldown;
    float timeout;
    public float arrowSpeed;
    public GameObject arrowPrefab;

    GameObject player;

    void Start()
    {
        timeout = cooldown;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            if (timeout <= 0) 
            {
                timeout = cooldown;

                Vector2 playerPos = player.transform.position;
                Vector2 myPos = transform.position;
                Vector2 direction = (playerPos - myPos).normalized;

                GameObject newArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                newArrow.GetComponent<ProjectileMovement>().velocity = direction * arrowSpeed;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                newArrow.transform.rotation = Quaternion.Euler(0, 0, angle - 45f);
            }
            
            if (timeout > 0)
            {
                timeout -= Time.deltaTime;
            }
        }
    }
}