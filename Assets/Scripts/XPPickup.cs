using UnityEngine;

public class XPPickup : MonoBehaviour
{
    GameObject player;
    public int total_xp;
    PlayerXP player_xp_component;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player_xp_component = player.GetComponent<PlayerXP>();

    }

    void OnTriggerEnter2D(Collider2D collision){
        player_xp_component.AddXP(total_xp);
        Destroy(this.transform.parent.gameObject);
    }
}
