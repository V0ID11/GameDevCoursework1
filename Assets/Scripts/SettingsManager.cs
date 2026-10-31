using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public PlayerLevellingStats masterSettingsTemplate; 
    [HideInInspector] public PlayerLevellingStats settings; 
    void Awake()
    {
        if (masterSettingsTemplate != null)
        {
            
            settings = Instantiate(masterSettingsTemplate);
        }
        else
        {
            Debug.LogError("Master Settings Template is missing!");
        }
    }
}