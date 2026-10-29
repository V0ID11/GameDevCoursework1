using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float RotationSpeed;
    SettingsManager settingsManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        settingsManager = this.transform.parent.gameObject.GetComponent<SettingsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f,0.0f, RotationSpeed * Time.deltaTime * settingsManager.settings.SpinSpeed, Space.Self);
        
    }
}
