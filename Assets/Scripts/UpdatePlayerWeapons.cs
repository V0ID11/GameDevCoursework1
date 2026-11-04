using UnityEngine;

public class UpdatePlayerWeapons : MonoBehaviour
{
    SettingsManager settingsManager;
    public GameObject sword; 
    public GameObject dagger;
    public GameObject bow;
    public GameObject aimLine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         settingsManager = GetComponent<SettingsManager>();
         UpdateWeapons();
    }

    public void UpdateWeapons()
    {
        if (settingsManager == null || settingsManager.settings == null)
        {
            Debug.LogWarning("SettingsManager or settings data is missing!");
            return;
        }

        // Sword Visuals 
        if (sword != null)
        {
            sword.GetComponent<SpriteRenderer>().sprite = settingsManager.settings.SwordSprite;
            sword.transform.localScale = new Vector3(settingsManager.settings.WeaponSize, settingsManager.settings.WeaponSize, 1);
        }

        // Bow Visuals
        if (bow != null)
        {
            bow.GetComponent<SpriteRenderer>().sprite = settingsManager.settings.BowSprite;
        }

        // Dagger Visuals
        if (dagger != null)
        {
            if (settingsManager.settings.ExtraDaggerEnabled)
            {
                dagger.SetActive(true);
                dagger.GetComponent<SpriteRenderer>().sprite = settingsManager.settings.DaggerSprite;
                dagger.transform.localScale = new Vector3(settingsManager.settings.ExtraDaggerSize, settingsManager.settings.ExtraDaggerSize, 1);
            }
            else
            {
                dagger.SetActive(false);
            }
        }

        // Aim Line Visuals (Add logic here if needed)
        if (aimLine != null){
            if (settingsManager.settings.AimLineEnabled){
                aimLine.SetActive(true);
                aimLine.GetComponent<AimLineController>().maxAimDistance = settingsManager.settings.AimLineSize;
            }
        }
    }
}