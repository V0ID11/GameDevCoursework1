using UnityEngine;

public class WeaponHit : MonoBehaviour
{

    Rigidbody2D rb;
    Health playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = transform.parent.GetComponent<Health>();
    }

    void OnTriggerEnter2D(Collider2D collision){
        
        bool isSibling = collision.transform.parent != null && 
                        collision.transform.parent == transform.parent && 
                        collision != gameObject;
        if (!isSibling){
            playerHealth.SubtractHealth(collision.transform.GetComponent<Weapon>().damage);
        }
    }
    
}
