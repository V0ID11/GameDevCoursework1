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

    void Start()
    {
        timeout = cooldown;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null){
        if (timeout <= 0) {
            timeout = cooldown;

            Vector3 playerPos = player.transform.position;

            Vector2 direction = (playerPos - transform.position).normalized;

            GameObject newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

            newArrow.GetComponent<ProjectileMovement>().velocity = direction * arrowSpeed;

            newArrow.transform.Rotate(0, 0, -45);
        }
        // Debug.Log(timeout);
        if (timeout > 0){timeout = timeout - Time.deltaTime;}
        
    }}
}

