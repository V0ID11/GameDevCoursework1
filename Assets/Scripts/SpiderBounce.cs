using UnityEngine;

public class SpiderBounce : MonoBehaviour
{
    public float bounceSpeed = 5f;
    public float crawlSpeed = 3f;
    
    public float minCrawlTime = 2f;
    public float maxCrawlTime = 4f;

    private Rigidbody2D rb;
    private bool isCrawling = false;
    private float crawlTimer = 0f;
    private Vector2 currentNormal;
    private Vector2 crawlDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb.linearVelocity == Vector2.zero)
        {
            rb.linearVelocity = Random.insideUnitCircle.normalized * bounceSpeed;
        }
    }

    void Update()
    {
        if (isCrawling)
        {
            crawlTimer -= Time.deltaTime;
            
            if (crawlTimer <= 0)
            {
                BounceOffWall();
            }
            else
            {
                rb.linearVelocity = crawlDirection * crawlSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;

        if (!isCrawling)
        {
            StartCrawling(normal);
        }
        else
        {
            StartCrawling(normal);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (isCrawling)
        {
            BounceOffWall();
        }
    }

    private void StartCrawling(Vector2 wallNormal)
    {
        isCrawling = true;
        currentNormal = wallNormal;
        
        crawlTimer = Random.Range(minCrawlTime, maxCrawlTime);

        float dirMultiplier = Random.value > 0.5f ? 1f : -1f; 
        crawlDirection = new Vector2(-wallNormal.y, wallNormal.x) * dirMultiplier;
        
        rb.linearVelocity = crawlDirection * crawlSpeed;
    }

    private void BounceOffWall()
    {
        isCrawling = false;
        
        Vector2 bounceDir = currentNormal + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        rb.linearVelocity = bounceDir.normalized * bounceSpeed;
    }
}