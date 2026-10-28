using UnityEngine;

public class XPPlayerFollowing : MonoBehaviour
{
    GameObject player;
    bool followingPlayer = false;
    Rigidbody2D rb;

    public float speed;

    int upDirection;
    int sideDirection;

    void Start(){
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        followingPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {   
        if (followingPlayer){
            if (player != null){
                Vector3 playerPos = player.transform.position;

                upDirection = (playerPos.y >= this.transform.position.y ) ? 1 : -1;
                sideDirection = (playerPos.x >= this.transform.position.x) ? 1 : -1;

                float angle = Mathf.Atan(Mathf.Abs(playerPos.y - this.transform.position.y) / Mathf.Abs(playerPos.x - this.transform.position.x));

                rb.linearVelocity = new Vector2(Mathf.Cos(angle) * speed * sideDirection, Mathf.Sin(angle) * speed * upDirection);
            }
        }
    }
}
