using UnityEngine;

public class WallBounce : MonoBehaviour
{
    public bool isVertical;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("Collided with: " + collision.gameObject.name);

        Bounce bounceScript = collision.gameObject.GetComponent<Bounce>();

        if (isVertical) {
            Vector2 newVelocity = new Vector2(bounceScript.velocity.x * -1, bounceScript.velocity.y);
            bounceScript.velocity = newVelocity;
        } else {
            Vector2 newVelocity = new Vector2(bounceScript.velocity.x, bounceScript.velocity.y * -1);
            bounceScript.velocity = newVelocity;
        }
    }
}
