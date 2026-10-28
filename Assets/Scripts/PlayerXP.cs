using UnityEngine;


public class PlayerXP : MonoBehaviour
{
    public int total_xp;
    public int xp;
    public int level;
    int for_next_level;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        total_xp = 0;
        xp = total_xp;
        level = 1;
        for_next_level = 1;
    }

    void level_up(){
        xp = xp - for_next_level;
        for_next_level = for_next_level * 2;
        level += 1;
    }

    public void AddXP(int incoming_xp){
        total_xp += incoming_xp;
        xp += incoming_xp;
        if (xp >= for_next_level){
            level_up();
        }
    }
}
