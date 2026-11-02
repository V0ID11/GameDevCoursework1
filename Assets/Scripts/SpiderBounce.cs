using UnityEngine;

public class SpiderBounce : MonoBehaviour
{
    public float totalVelocity = 5f;
    public float crawlSpeed = 6f;
    
    public float minCrawlTime = 1f;
    public float maxCrawlTime = 2f;

    private Rigidbody2D rb;
    private bool isCrawling = false;
    private float crawlTimer = 0f;
    private Vector2 currentNormal;
    private Vector2 crawlDirection;

    private Vector2 lastVelocity;
    private Vector2 plannedBounceVelocity;

    private int wallsTouching = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        Vector2 initialVelocity = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        rb.linearVelocity = CalculateBounceVelocity(initialVelocity);
    }

    void Update()
    {
        if (!isCrawling)
        {
            lastVelocity = rb.linearVelocity;
        }
        else
        {
            crawlTimer -= Time.deltaTime;
            
            if (crawlTimer <= 0)
            {
                BounceOffWall();
            }
        }
        if (rb.linearVelocity.magnitude == 0)
        {
            BounceOffWall();
        }
    }

    void FixedUpdate()
    {
        if (isCrawling)
        {
            rb.linearVelocity = (crawlDirection * crawlSpeed) + (-currentNormal * 2f);
        }
    }

    private bool IsWall(Collision2D collision)
    {
        Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
        return otherRb == null || otherRb.bodyType != RigidbodyType2D.Dynamic;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;

        if (!IsWall(collision))
        {
            if (isCrawling)
            {
                crawlDirection *= -1f;
                
                rb.linearVelocity = (crawlDirection * crawlSpeed) + (-currentNormal * 2f);
                return;
            }
            else
            {
                Vector2 reflected = Vector2.Reflect(lastVelocity, normal);
                
                isCrawling = false; 
                wallsTouching = 0;
                rb.linearVelocity = CalculateBounceVelocity(reflected);
                return; 
            }
        }

        wallsTouching++;

        if (!isCrawling)
        {   
            Vector2 reflected = Vector2.Reflect(lastVelocity, normal);
            plannedBounceVelocity = CalculateBounceVelocity(reflected);
            StartCrawling(normal, lastVelocity, false);
        }
        else
        {
            Vector2 reflected = Vector2.Reflect(rb.linearVelocity, normal);
            plannedBounceVelocity = CalculateBounceVelocity(reflected);
            StartCrawling(normal, rb.linearVelocity, true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        wallsTouching--;
        if (isCrawling && wallsTouching <= 0)
        {
            wallsTouching = 0;
            BounceOffWall();
        }
    }

    private void StartCrawling(Vector2 wallNormal, Vector2 incomingVelocity, bool isCorner)
    {
        isCrawling = true;
        currentNormal = wallNormal;
        
        if (!isCorner)
        {
            crawlTimer = Random.Range(minCrawlTime, maxCrawlTime);
        }

        Vector2 parallelVector = new Vector2(-wallNormal.y, wallNormal.x);

        float dirMultiplier = Mathf.Sign(Vector2.Dot(incomingVelocity, parallelVector));

        crawlDirection = new Vector2(-wallNormal.y, wallNormal.x) * dirMultiplier;
        
        rb.linearVelocity = (crawlDirection * crawlSpeed) + (-currentNormal * 2f);
    }

    private void BounceOffWall()
    {
        isCrawling = false;

        wallsTouching = 0;
        
        rb.linearVelocity = plannedBounceVelocity.normalized * totalVelocity;
    }

    private Vector2 CalculateBounceVelocity(Vector2 inVelocity)
    {
        int upDirection = (inVelocity.y >= 0) ? 1 : -1;
        int sideDirection = (inVelocity.x >= 0) ? 1 : -1;

        if (Mathf.Abs(inVelocity.y) < 0.1f) 
        {
            inVelocity.y = 0.5f * upDirection * 5f;
        }

        if (Mathf.Abs(inVelocity.x) < 0.1f) 
        {
            inVelocity.x = 0.5f * sideDirection;
        }

        upDirection = (inVelocity.y >= 0) ? 1 : -1;
        sideDirection = (inVelocity.x >= 0) ? 1 : -1;

        float angle = Mathf.Atan(Mathf.Abs(inVelocity.y) / Mathf.Abs(inVelocity.x));

        return new Vector2(
            Mathf.Cos(angle) * totalVelocity * sideDirection, 
            Mathf.Sin(angle) * totalVelocity * upDirection
        );
    }
}