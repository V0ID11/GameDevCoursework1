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

        // 1. Get 1 Valid Weapon Upgrade (Sword or Bow)
        List<UpgradeInformation> validWeapons = new List<UpgradeInformation>();
        foreach (var up in allSwordUpgrades) if (CanPurchaseUpgrade(up)) validWeapons.Add(up);
        foreach (var up in allBowUpgrades) if (CanPurchaseUpgrade(up)) validWeapons.Add(up);

        if (validWeapons.Count > 0)
        {
            UpgradeInformation pickedWeapon = validWeapons[Random.Range(0, validWeapons.Count)];
            choices.Add(pickedWeapon);
        }

        // 2. Get 1 Valid Passive Upgrade
        List<UpgradeInformation> validPassives = new List<UpgradeInformation>();
        foreach (var up in allPassiveUpgrades) if (CanPurchaseUpgrade(up)) validPassives.Add(up);

        if (validPassives.Count > 0)
        {
            UpgradeInformation pickedPassive = validPassives[Random.Range(0, validPassives.Count)];
            choices.Add(pickedPassive);
        }

        // 3. Get 1 Truly Random Remaining Valid Upgrade
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
        // Passives don't use Bloons path restrictions, just check max tier logic if needed
        if (upgrade.category == UpgradeInformation.UpgradeCategory.Passive)
        {
            string passiveKey = "Passive_" + upgrade.upgradeID;
            int currentPassiveTier = unlockedTiers.ContainsKey(passiveKey) ? unlockedTiers[passiveKey] : 0;
            return upgrade.tier == currentPassiveTier + 1;
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
        // Commit progression
        string trackingKey = chosenUpgrade.category == UpgradeInformation.UpgradeCategory.Passive 
            ? "Passive_" + chosenUpgrade.upgradeID 
            : chosenUpgrade.category.ToString() + "_" + chosenUpgrade.pathID;
            
        unlockedTiers[trackingKey] = chosenUpgrade.tier;

        if (chosenUpgrade.tier >= 3)
        {
            if (chosenUpgrade.category == UpgradeInformation.UpgradeCategory.Sword) lockedSwordPath = chosenUpgrade.pathID;
            if (chosenUpgrade.category == UpgradeInformation.UpgradeCategory.Bow) lockedBowPath = chosenUpgrade.pathID;
        }

        // Apply stats to PlayerLevellingStats asset via SettingsManager
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
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    public void ApplyUpgradeEffect(UpgradeInformation upgrade, PlayerLevellingStats playerStats)
    {
        foreach (var mod in upgrade.modifiers)
        {
            ApplyModifier(playerStats, mod);
        }
        Debug.Log($"Upgraded {upgrade.upgradeName} to Tier {upgrade.tier}");
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