using UnityEngine;

public class FireballExplosion : MonoBehaviour
{
    public float fadeDuration = 0.3f;
    
    private SpriteRenderer sr;
    private float timer = 0f;
    private Weapon weaponComponent;

    void Start()
    {
        AudioManager.instance.PlaySFX(SoundType.Explode);
        sr = GetComponent<SpriteRenderer>();
        weaponComponent = GetComponent<Weapon>(); // Grab the Weapon script on the explosion
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
        
        if (sr != null)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }

        if (timer >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Use the Weapon component to calculate the damage
        if (collision.gameObject.CompareTag("Player") && weaponComponent != null)
        {
            if (collision.gameObject.TryGetComponent<Health>(out Health playerHealth))
            {
                playerHealth.SubtractHealth(weaponComponent.GetWeaponDamage());
            }
        }
    }
}