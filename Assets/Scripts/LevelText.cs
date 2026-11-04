using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LevelCounterUI : MonoBehaviour
{
    [Header("References")]
    public OrchSpawner spawner;
    
    private TextMeshProUGUI levelText;
    private int lastRecordedLevel = -1;

    void Awake()
    {
        levelText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (spawner != null)
        {
            if (spawner.currentlevel != lastRecordedLevel)
            {
                lastRecordedLevel = spawner.currentlevel;
                levelText.text = "Rounds\nSurvived: " + lastRecordedLevel.ToString();
            }
        }
        else
        {
            Debug.LogWarning("OrchSpawner is not assigned to the LevelCounterUI script on " + gameObject.name);
        }
    }
}