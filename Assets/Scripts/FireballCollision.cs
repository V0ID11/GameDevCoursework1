using UnityEngine;

public class FireballCollision : MonoBehaviour
{
    public GameObject explosionPrefab;
    
    Rigidbody2D rb;
    Weapon weaponComponent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponComponent = GetComponent<Weapon>(); // Fetch the Weapon script shown in the inspector
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            Vector2 v = rb.linearVelocity;
            rb.rotation = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 45f; 
        }
    }

    void ExplodeAndDestroy()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            
            // Pass the projectile's damage directly into the explosion's Weapon script
            if (weaponComponent != null && explosion.TryGetComponent<Weapon>(out Weapon expWeapon))
            {
                expWeapon.SetWeaponDamage(weaponComponent.GetWeaponDamage());
            }
        }
        
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore other enemy projectiles if needed
        if (collision.gameObject.CompareTag("Arrow"))
        {
            return;
        }

        // Deal direct hit damage before exploding
        if (collision.gameObject.CompareTag("Player") && weaponComponent != null)
        {
            if (collision.gameObject.TryGetComponent<Health>(out Health playerHealth))
            {
                playerHealth.SubtractHealth(weaponComponent.GetWeaponDamage());
            }
        }
        ExplodeAndDestroy();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player") && weaponComponent != null)
        {
            if (collision.gameObject.TryGetComponent<Health>(out Health playerHealth))
            {
                playerHealth.SubtractHealth(weaponComponent.GetWeaponDamage());
            }
        }
        ExplodeAndDestroy();
    }
}