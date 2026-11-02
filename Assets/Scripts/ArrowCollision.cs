using UnityEngine;
using System.Collections.Generic;

public class ArrowCollision : MonoBehaviour
{
    public int pierce = 0;
    public int ricochet = 0;

    Rigidbody2D rb;
    Vector2 preStepVelocity;
    
    // Tracks enemies already hit by this arrow to prevent multiple triggers from the same body
    private HashSet<Collider2D> hitEnemies = new HashSet<Collider2D>();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        preStepVelocity = rb.linearVelocity;

        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            Vector2 v = rb.linearVelocity;
            rb.rotation = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 45f;
        }
    }

    // Handles solid, non-trigger objects (like Walls)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Sword") ||
            collision.gameObject.name.Contains("Dagger") ||
            collision.gameObject.CompareTag("Arrow"))
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            AudioManager.instance.PlaySFX(SoundType.Bounce);
            if (ricochet > 0)
            {
                ricochet--;
                SyncAfterBounce();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // Handles trigger objects (like EnemyBody)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Sword") ||
            collision.gameObject.name.Contains("Dagger") ||
            collision.gameObject.CompareTag("Arrow"))
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyBody"))
        {
            // Ensure we don't double-count the same trigger collider in a single pass
            if (hitEnemies.Contains(collision)) return;
            hitEnemies.Add(collision);

            Debug.Log("Here");
            if (pierce > 0)
            {
                pierce--;
                
                // Keep the original momentum/velocity
                rb.linearVelocity = preStepVelocity;
                SyncAfterBounce();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void SyncAfterBounce()
    {
        Vector2 v = rb.linearVelocity;

        var movement = GetComponent<ProjectileMovement>();
        if (movement != null) movement.velocity = v;

        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 45f;
    }
}