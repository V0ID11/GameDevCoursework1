using UnityEngine;
using System; 

public class PlayerXP : MonoBehaviour
{
    public int total_xp;
    public int xp;
    public int level;
    public int for_next_level; 

    public Action<int, int> OnXPChanged;

    void Start()
    {
        total_xp = 0;
        xp = 0; 
        level = 1;
        
        OnXPChanged?.Invoke(xp, for_next_level);
    }

    void level_up()
    {
        xp = xp - for_next_level;
        for_next_level = for_next_level * 2;
        level += 1;
    }

    public void AddXP(int incoming_xp)
    {
        total_xp += incoming_xp;
        xp += incoming_xp;
        
        while (xp >= for_next_level)
        {
            level_up();
            Debug.Log("level up");
        }

        OnXPChanged?.Invoke(xp, for_next_level);
    }
}