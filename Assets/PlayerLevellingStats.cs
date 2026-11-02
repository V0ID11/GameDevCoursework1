using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLevellingStats", menuName = "Scriptable Objects/PlayerLevellingStats")]
public class PlayerLevellingStats : ScriptableObject
{
    public int SwordDamage;
    public float WeaponSize;
    public float SpinSpeed;

    public bool ExtraDaggerEnabled;
    public int ExtraDaggerDamage;
    public float ExtraDaggerSize;

    public float SwordAbilityCooldown;


    public bool AimLineEnabled;
    public int AimLineSize;
    public float ArrowSpeed;

    public int ArrowDamage;
    public float ArrowShotCooldown;

    public int ArrowPierce;
    public int ArrowRicochet;

    public float PlayerSpeed;
    public float DamageReductionRatio;
    public float DashAbilityCooldown;
    public float FocusAbilityCooldown;
    public int total_health;
    public int current_health;

    public bool shotCooldownReduction;
    public bool SwordAbilityCooldownReduction;

    

    public Sprite SwordSprite;
    public Sprite DaggerSprite;
    public Sprite BowSprite;


}
