using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("References")]
    public SettingsManager settingsManager;  // Reference to get PlayerLevellingStats

    [Header("UI References")]
    public GameObject upgradePanel;          
    public Transform cardsParent;            
    public GameObject upgradeCardPrefab;     

    [Header("Master Upgrade Pools")]
    public List<UpgradeInformation> allSwordUpgrades;
    public List<UpgradeInformation> allBowUpgrades;
    public List<UpgradeInformation> allPassiveUpgrades;

    // Progression tracking dictionaries
    private Dictionary<string, int> unlockedTiers = new Dictionary<string, int>();
    private int lockedSwordPath = -1;
    private int lockedBowPath = -1;

    // Keep track of current choices displayed on screen
    private List<UpgradeInformation> currentChoices = new List<UpgradeInformation>();

    public void TriggerLevelUp()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        upgradePanel.SetActive(true);
        DisplayChoices();
    }

    private void DisplayChoices()
    {
        foreach (Transform child in cardsParent)
        {
            Destroy(child.gameObject);
        }

        currentChoices = GenerateLevelUpChoices();

        foreach (var upgrade in currentChoices)
        {
            GameObject cardObj = Instantiate(upgradeCardPrefab, cardsParent);
            UpgradeCardUI cardUI = cardObj.GetComponent<UpgradeCardUI>();
            cardUI.Setup(upgrade, this);
        }
    }

    private List<UpgradeInformation> GenerateLevelUpChoices()
    {
        List<UpgradeInformation> choices = new List<UpgradeInformation>();

        List<UpgradeInformation> validWeapons = new List<UpgradeInformation>();
        foreach (var up in allSwordUpgrades) if (CanPurchaseUpgrade(up)) validWeapons.Add(up);
        foreach (var up in allBowUpgrades) if (CanPurchaseUpgrade(up)) validWeapons.Add(up);

        if (validWeapons.Count > 0)
        {
            UpgradeInformation pickedWeapon = validWeapons[Random.Range(0, validWeapons.Count)];
            choices.Add(pickedWeapon);
        }

        List<UpgradeInformation> validPassives = new List<UpgradeInformation>();
        foreach (var up in allPassiveUpgrades) if (CanPurchaseUpgrade(up)) validPassives.Add(up);

        if (validPassives.Count > 0)
        {
            UpgradeInformation pickedPassive = validPassives[Random.Range(0, validPassives.Count)];
            choices.Add(pickedPassive);
        }

        List<UpgradeInformation> allValid = new List<UpgradeInformation>();
        foreach (var up in allSwordUpgrades) if (CanPurchaseUpgrade(up) && !choices.Contains(up)) allValid.Add(up);
        foreach (var up in allBowUpgrades) if (CanPurchaseUpgrade(up) && !choices.Contains(up)) allValid.Add(up);
        foreach (var up in allPassiveUpgrades) if (CanPurchaseUpgrade(up) && !choices.Contains(up)) allValid.Add(up);

        if (allValid.Count > 0)
        {
            choices.Add(allValid[Random.Range(0, allValid.Count)]);
        }

        return choices;
    }

    private bool CanPurchaseUpgrade(UpgradeInformation upgrade)
    {
        if (upgrade.category == UpgradeInformation.UpgradeCategory.Passive)
        {
            string passiveKey = "Passive_" + upgrade.upgradeID;
            int currentPassiveTier = unlockedTiers.ContainsKey(passiveKey) ? unlockedTiers[passiveKey] : 0;

            // If maxTier is set to 0 or less, treat it as infinite stacking
            if (upgrade.maxTier <= 0)
            {
                return true; 
            }

            // Otherwise, respect the maximum tier limit
            return currentPassiveTier < upgrade.maxTier;
        }

        string pathKey = upgrade.category.ToString() + "_" + upgrade.pathID;
        int currentTierOnPath = unlockedTiers.ContainsKey(pathKey) ? unlockedTiers[pathKey] : 0;
        
        if (upgrade.tier != currentTierOnPath + 1) return false;
        
        if (upgrade.tier >= 3)
        {
            if (upgrade.category == UpgradeInformation.UpgradeCategory.Sword)
            {
                if (lockedSwordPath != -1 && lockedSwordPath != upgrade.pathID) return false;
            }
            else if (upgrade.category == UpgradeInformation.UpgradeCategory.Bow)
            {
                if (lockedBowPath != -1 && lockedBowPath != upgrade.pathID) return false;
            }
        }

        return true;
    }

    public void SelectUpgrade(UpgradeInformation chosenUpgrade)
    {
        string trackingKey;

        if (chosenUpgrade.category == UpgradeInformation.UpgradeCategory.Passive)
        {
            trackingKey = "Passive_" + chosenUpgrade.upgradeID;
            int currentTier = unlockedTiers.ContainsKey(trackingKey) ? unlockedTiers[trackingKey] : 0;
            
            // Increment the tier infinitely for passives
            unlockedTiers[trackingKey] = currentTier + 1;
        }
        else
        {
            trackingKey = chosenUpgrade.category.ToString() + "_" + chosenUpgrade.pathID;
            unlockedTiers[trackingKey] = chosenUpgrade.tier;

            if (chosenUpgrade.tier >= 3)
            {
                if (chosenUpgrade.category == UpgradeInformation.UpgradeCategory.Sword) lockedSwordPath = chosenUpgrade.pathID;
                if (chosenUpgrade.category == UpgradeInformation.UpgradeCategory.Bow) lockedBowPath = chosenUpgrade.pathID;
            }
        }

        if (settingsManager != null && settingsManager.settings != null)
        {
            ApplyUpgradeEffect(chosenUpgrade, settingsManager.settings);
        }
        else
        {
            Debug.LogError("SettingsManager or PlayerLevellingStats reference is missing on UpgradeManager!");
        }

        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ApplyUpgradeEffect(UpgradeInformation upgrade, PlayerLevellingStats playerStats)
    {
        foreach (var mod in upgrade.modifiers)
        {
            ApplyModifier(playerStats, mod);
        }
        Debug.Log($"Upgraded {upgrade.upgradeName}");
        
        if (upgrade.category == UpgradeInformation.UpgradeCategory.Sword) {
            if (upgrade.newSprite != null) {
                playerStats.SwordSprite = upgrade.newSprite;
            }
            if (playerStats.ExtraDaggerEnabled){
                playerStats.DaggerSprite = playerStats.SwordSprite;
            }
        } else if (upgrade.category == UpgradeInformation.UpgradeCategory.Bow){
            if (upgrade.newSprite != null) {
                playerStats.BowSprite = upgrade.newSprite;
            }
        }
        
        GameObject.FindWithTag("Player").GetComponent<UpdatePlayerWeapons>().UpdateWeapons();
    }

    private void ApplyModifier(PlayerLevellingStats stats, StatModifier mod)
    {
        switch (mod.targetStat)
        {
            case StatType.SwordDamage: stats.SwordDamage = (int)ApplyMath(stats.SwordDamage, mod); break;
            case StatType.WeaponSize: stats.WeaponSize = ApplyMath(stats.WeaponSize, mod); break;
            case StatType.SpinSpeed: stats.SpinSpeed = ApplyMath(stats.SpinSpeed, mod); break;
            case StatType.ExtraDaggerEnabled: stats.ExtraDaggerEnabled = mod.value > 0; break;
            case StatType.ExtraDaggerDamage: stats.ExtraDaggerDamage = (int)ApplyMath(stats.ExtraDaggerDamage, mod); break;
            case StatType.ExtraDaggerSize: stats.ExtraDaggerSize = ApplyMath(stats.ExtraDaggerSize, mod); break;
            case StatType.SwordAbilityCooldown: stats.SwordAbilityCooldown = ApplyMath(stats.SwordAbilityCooldown, mod); break;
            case StatType.AimLineEnabled: stats.AimLineEnabled = mod.value > 0; break;
            case StatType.AimLineSize: stats.AimLineSize = (int)ApplyMath(stats.AimLineSize, mod); break;
            case StatType.ArrowSpeed: stats.ArrowSpeed = ApplyMath(stats.ArrowSpeed, mod); break;
            case StatType.ArrowDamage: stats.ArrowDamage = (int)ApplyMath(stats.ArrowDamage, mod); break;
            case StatType.ArrowShotCooldown: stats.ArrowShotCooldown = ApplyMath(stats.ArrowShotCooldown, mod); break;
            case StatType.ArrowPierce: stats.ArrowPierce = (int)ApplyMath(stats.ArrowPierce, mod); break;
            case StatType.ArrowRicochet: stats.ArrowRicochet = (int)ApplyMath(stats.ArrowRicochet, mod); break;
            case StatType.PlayerSpeed: stats.PlayerSpeed = ApplyMath(stats.PlayerSpeed, mod); break;
            case StatType.DamageReductionRatio: stats.DamageReductionRatio = ApplyMath(stats.DamageReductionRatio, mod); break;
            case StatType.DashAbilityCooldown: stats.DashAbilityCooldown = ApplyMath(stats.DashAbilityCooldown, mod); break;
            case StatType.FocusAbilityCooldown: stats.FocusAbilityCooldown = ApplyMath(stats.FocusAbilityCooldown, mod); break;
            case StatType.total_health: stats.total_health = (int)ApplyMath(stats.total_health, mod); break;
            case StatType.current_health: stats.current_health = (int)ApplyMath(stats.current_health, mod); break;
            case StatType.shotCooldownReduction: stats.shotCooldownReduction = mod.value > 0; break;
            case StatType.SwordAbilityCooldownReduction: stats.SwordAbilityCooldownReduction = mod.value > 0; break;
        }
    }

    private float ApplyMath(float currentVal, StatModifier mod)
    {
        return mod.modifyMode switch
        {
            ModifyMode.AddFlat => currentVal + mod.value,
            ModifyMode.Multiply => currentVal * mod.value,
            _ => currentVal
        };
    }
}