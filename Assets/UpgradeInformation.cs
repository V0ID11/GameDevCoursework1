using UnityEngine;

[System.Serializable]
public struct StatModifier
{
    public StatType targetStat;
    public ModifyMode modifyMode;
    public float value;
}

[CreateAssetMenu(fileName = "UpgradeInformation", menuName = "Scriptable Objects/UpgradeInformation")]
public class UpgradeInformation : ScriptableObject
{
    public enum UpgradeCategory { Sword, Bow, Passive }
    
    [Header("Bloons Branching Rules")]
    public UpgradeCategory category;
    public int pathID;     
    public int tier;   
    public int maxTier;    

    [Header("UI Info")]
    public string upgradeID;
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("Effects")]
    public StatModifier[] modifiers;

    public Sprite newSprite;
}

public enum StatType
{
    SwordDamage,
    WeaponSize,
    SpinSpeed,
    ExtraDaggerEnabled,
    ExtraDaggerDamage,
    ExtraDaggerSize,
    SwordAbilityCooldown,
    AimLineEnabled,
    AimLineSize,
    ArrowSpeed,
    ArrowDamage,
    ArrowShotCooldown,
    ArrowPierce,
    ArrowRicochet,
    PlayerSpeed,
    DamageReductionRatio,
    DashAbilityCooldown,
    FocusAbilityCooldown,
    total_health,
    current_health,
    shotCooldownReduction,
    SwordAbilityCooldownReduction
}

public enum ModifyMode
{
    AddFlat,      
    Multiply,     
    Toggle       
}