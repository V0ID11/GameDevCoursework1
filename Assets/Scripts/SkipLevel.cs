using UnityEngine;

public class SkipLevel : MonoBehaviour
{

    public OrchSpawner spawner;
    public void SkipLevelButton()
    {
        spawner.round_timer = 1f;
    }

    void Update()
    {
        if (spawner.currentlevel > 0)
        {
            gameObject.SetActive(false);
        }
    }
}
