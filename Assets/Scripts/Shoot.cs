using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    InputAction mouseAction;    
    public float cooldown;
    public float timeout = 0;
    public float arrowSpeed;
    public GameObject arrowPrefab;

    SettingsManager settingsManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseAction = InputSystem.actions.FindAction("Shoot");

        if (!CompareTag("Arrow"))
        {
            if (transform.parent != null && transform.parent != null)
            {
                settingsManager = transform.parent.GetComponent<SettingsManager>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseAction.triggered && (timeout <= 0)) {
            timeout = cooldown * settingsManager.settings.ArrowShotCooldown;

            
            Vector2 screenPos = Pointer.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            
            Vector2 direction = (worldPos - (Vector2)transform.position).normalized;
            

            GameObject newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

            newArrow.GetComponent<ProjectileMovement>().velocity = direction * arrowSpeed * settingsManager.settings.ArrowSpeed;

            newArrow.transform.Rotate(0, 0, -45);
            newArrow.GetComponent<Weapon>().SetWeaponDamage(settingsManager.settings.ArrowDamage);
            newArrow.GetComponent<ArrowCollision>().pierce = settingsManager.settings.ArrowPierce;
            newArrow.GetComponent<ArrowCollision>().ricochet = settingsManager.settings.ArrowRicochet;

            AudioManager.instance.PlaySFX(SoundType.Arrow);

        }
        // Debug.Log(timeout);
        if (timeout > 0){timeout = timeout - Time.deltaTime;}
        
    }
}