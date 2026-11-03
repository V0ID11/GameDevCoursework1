using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    GameObject player;
    public int total_xp;
    Health player_hp_component;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player_hp_component = player.GetComponent<Health>();

    }

    void OnTriggerEnter2D(Collider2D collision){
        player_hp_component.SubtractHealth(-5);
        AudioManager.instance.PlaySFX(SoundType.XP);
        Destroy(this.transform.parent.gameObject);
    }
}
