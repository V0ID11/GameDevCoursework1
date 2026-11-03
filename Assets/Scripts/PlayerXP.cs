using UnityEngine;
using System; 

public class PlayerXP : MonoBehaviour
{
    public int total_xp;
    public int xp;
    public int level;
    public int for_next_level = 1; // Set a default base requirement so it doesn't stay 0!

    public Action<int, int> OnXPChanged;

    public UpgradeManager upgradeManager;

    void Start()
    {
        total_xp = 0;
        xp = 0; 
        level = 1;
        
        // Ensure for_next_level has a valid value if not set in inspector
        if (for_next_level <= 0) 
        {
            for_next_level = 100;
        }

        OnXPChanged?.Invoke(xp, for_next_level);
    }

    void level_up()
    {
        xp = xp - for_next_level;
        for_next_level = for_next_level * 2;
        level += 1;

        // Safely play sound only if AudioManager exists
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(SoundType.LevelUp);
        }

        // Trigger the upgrade selection screen
        if (upgradeManager != null)
        {
            upgradeManager.TriggerLevelUp();
        }
        else
        {
            Debug.LogWarning("UpgradeManager reference not set on PlayerXP!");
        }
    }

    public void AddXP(int incoming_xp)
    {
        total_xp += incoming_xp;
        xp += incoming_xp;
        
        // Safety guard against infinite loops if for_next_level is accidentally 0
        int safetyCounter = 0;
        while (xp >= for_next_level && safetyCounter < 100)
        {
            level_up();
            Debug.Log("level up");
            safetyCounter++;
        }

        OnXPChanged?.Invoke(xp, for_next_level);
    }
}