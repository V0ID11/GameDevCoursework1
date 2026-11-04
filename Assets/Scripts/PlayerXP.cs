using UnityEngine;
using System; 

public class PlayerXP : MonoBehaviour
{
    public int total_xp;
    public int xp;
    public int level;
    public int for_next_level;

    [Header("Upgrade Queuing")]
    public int pendingUpgrades = 0; 

    public Action<int, int> OnXPChanged;
    public UpgradeManager upgradeManager;

    void Start()
    {
        total_xp = 0;
        xp = 0; 
        level = 1;
        
        if (for_next_level <= 0) 
        {
            for_next_level = 1;
        }

        OnXPChanged?.Invoke(xp, for_next_level);
    }

    void Update()
    {
        CheckAndTriggerNextUpgrade();
    }

    void level_up()
    {
        xp = xp - for_next_level;
        for_next_level += 1;
        if(for_next_level > 5)
        {
            for_next_level = 5;
        }
        level += 1;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(SoundType.LevelUp);
        }

        pendingUpgrades++;
    }

    public void AddXP(int incoming_xp)
    {
        total_xp += incoming_xp;
        xp += incoming_xp;
        
        int safetyCounter = 0;
        while (xp >= for_next_level && safetyCounter < 100)
        {
            level_up();
            Debug.Log("level up tracked");
            safetyCounter++;
        }

        OnXPChanged?.Invoke(xp, for_next_level);
    }

    void CheckAndTriggerNextUpgrade()
    {
        // Only trigger if we have pending upgrades and an upgrade manager exists
        if (pendingUpgrades > 0 && upgradeManager != null)
        {
            // Check if the upgrade panel is currently inactive (meaning the player finished their last choice)
            if (upgradeManager.upgradePanel != null && !upgradeManager.upgradePanel.activeSelf)
            {
                pendingUpgrades--;
                upgradeManager.TriggerLevelUp();
            }
        }
        else if (pendingUpgrades > 0 && upgradeManager == null)
        {
            Debug.LogWarning("UpgradeManager reference not set on PlayerXP!");
            pendingUpgrades = 0;
        }
    }
}