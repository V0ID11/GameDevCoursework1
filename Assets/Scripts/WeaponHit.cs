using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    Health playerHealth;


    void Start()
    {
        playerHealth = transform.parent.GetComponent<Health>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore the owner and anything else under the same parent
        if (collision.transform.IsChildOf(transform.parent)) return;

        // The thing we hit must actually have a Weapon on it
        
        Weapon weapon = collision.GetComponent<Weapon>();
        if (weapon == null) return;

        playerHealth.SubtractHealth(weapon.GetWeaponDamage());
    }
}