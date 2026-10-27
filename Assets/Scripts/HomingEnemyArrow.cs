using UnityEngine;

public class HomingEnemyArrow : MonoBehaviour
{
    Rigidbody2D rb;
    public float ttl;
    public float speed;

    int upDirection;
    int sideDirection;
    GameObject player;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (ttl <= 0) {
            Destroy(gameObject);
        }
        Vector3 playerPos = player.transform.position;

        upDirection = (playerPos.y >= this.transform.position.y ) ? 1 : -1;
        sideDirection = (playerPos.x >= this.transform.position.x) ? 1 : -1;

        float angle = Mathf.Atan(Mathf.Abs(playerPos.y - this.transform.position.y) / Mathf.Abs(playerPos.x - this.transform.position.x));

        rb.linearVelocity = new Vector2(Mathf.Cos(angle) * speed * sideDirection * Time.deltaTime, Mathf.Sin(angle) * speed * upDirection * Time.deltaTime);

        ttl -= Time.deltaTime;
    }
}
