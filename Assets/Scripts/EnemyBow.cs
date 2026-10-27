using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBow : MonoBehaviour
{
    public float RotationSpeed = 720f; 
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (player != null){
        Vector3 playerPos = player.transform.position;

        Vector2 direction = playerPos - transform.position;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
    }
}
}