using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    Health playerHealth;
    SettingsManager playerStats;

    GameObject player;


    void Start()
    {
        playerHealth = transform.parent.GetComponent<Health>();
        player = GameObject.FindWithTag("Player");
        if (player != null){
        playerStats = player.GetComponent<SettingsManager>();
    }}

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore the owner and anything else under the same parent
        if (collision.transform.IsChildOf(transform.parent)) return;

        // The thing we hit must actually have a Weapon on it
        if (collision.gameObject.tag == "Player" || (collision.gameObject.tag == "Arrow" && transform.parent.tag != "Player")){
            if (playerStats.settings.shotCooldownReduction) {
                GameObject.Find("BowPivot").GetComponent<Shoot>().cooldown -= 2;
            } 
            if (playerStats.settings.SwordAbilityCooldownReduction){
                GameObject.Find("SwordPivot").GetComponent<SwordAttack>().cooldown -= 2;
            }
        }
        
        Weapon weapon = collision.GetComponent<Weapon>();
        if (weapon == null) return;

        playerHealth.SubtractHealth(weapon.GetWeaponDamage());
    }
}