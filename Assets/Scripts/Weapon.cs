using UnityEngine;

public class Weapon : MonoBehaviour
{
    SettingsManager settingsManager; 
    public int damage;

    void Start()
    {
        // Using CompareTag is safer and more efficient
        if (!CompareTag("Arrow"))
        {
            if (transform.parent != null && transform.parent.parent != null)
            {
                settingsManager = transform.parent.parent.GetComponent<SettingsManager>();
            }
        }
        
        // SetWeaponDamage(0);
    }

    public void SetWeaponDamage(int weaponDamage)
    {
        if (settingsManager != null)
        {
            if (gameObject.name == "Sword")
            {
                damage = settingsManager.settings.SwordDamage;
            }
            else if (gameObject.name == "Dagger")
            {
                damage = settingsManager.settings.ExtraDaggerDamage;
            }
        }
        else 
        {
            damage = weaponDamage; 
        }
    }

    public int GetWeaponDamage(){
        if (settingsManager != null)
        {
            if (gameObject.name == "Sword")
            {
                damage = settingsManager.settings.SwordDamage;
            }
            else if (gameObject.name == "Dagger")
            {
                damage = settingsManager.settings.ExtraDaggerDamage;
            } else {
                damage = settingsManager.settings.SwordDamage;
            }
        }
        
        return damage;
    }
}