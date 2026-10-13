using UnityEngine;
using UnityEngine.InputSystem;

public class Bounce : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 velocity;
    // public VelocityMult velocityMult;
    public int totalVelocity;
    int upDirection;
    int sideDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lv = rb.linearVelocity;


        upDirection = (lv.y >= 0) ? 1 : -1;
        sideDirection = (lv.x >= 0) ? 1 : -1;


        if (Mathf.Abs(lv.y) < (0.1f * GameSettings.GlobalSpeedMultplier)) {
            lv.y = 0.5f * GameSettings.GlobalSpeedMultplier * upDirection * 5;
        }

        if (Mathf.Abs(lv.x) < (0.1f * GameSettings.GlobalSpeedMultplier)) {
            lv.x = 0.5f * GameSettings.GlobalSpeedMultplier * sideDirection;
        }

        upDirection = (lv.y >= 0) ? 1 : -1;
        sideDirection = (lv.x >= 0) ? 1 : -1;

        float angle = Mathf.Atan(Mathf.Abs(lv.y) / Mathf.Abs(lv.x));

        float currentSpeed = totalVelocity * GameSettings.GlobalSpeedMultplier;
        rb.linearVelocity = new Vector2(
            Mathf.Cos(angle) * currentSpeed * sideDirection, 
            Mathf.Sin(angle) * currentSpeed * upDirection
        );
    }

    public void updateDirection(Vector2 mouseLocation){
        float angle = Mathf.Atan(Mathf.Abs(mouseLocation.y - transform.position.y) / (Mathf.Abs(mouseLocation.x - transform.position.x)));


        
        int upDirection = (mouseLocation.y - transform.position.y > 0) ? 1 : -1;
        int sideDirection = (mouseLocation.x - transform.position.x > 0) ? 1 : -1;
        
        rb.linearVelocity = new Vector2(Mathf.Cos(angle) * (totalVelocity * GameSettings.GlobalSpeedMultplier) * sideDirection, Mathf.Sin(angle) * (totalVelocity * GameSettings.GlobalSpeedMultplier) * upDirection);
    }
}
