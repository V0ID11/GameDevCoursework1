using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    public int pierce = 0;
    public int ricochet = 0;

    Rigidbody2D rb;
    Vector2 preStepVelocity;

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
        else
        {
            if (pierce > 0)
            {
                pierce--;
                
                rb.linearVelocity = preStepVelocity;
                SyncAfterBounce();
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
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

        // Keep ProjectileMovement's copy in sync if it stores one
        var movement = GetComponent<ProjectileMovement>();
        if (movement != null) movement.velocity = v;

        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 45f;
    }
}