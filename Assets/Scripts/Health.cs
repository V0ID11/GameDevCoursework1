using System; 
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int totalHealth;
    public int current_health;
    float i_frames=0.5f;
    float i_frames_count=0;
    public GameObject xp;
    private SpriteRenderer spriteRenderer;
    private Shader originalShader;
    private Shader whiteShader;
    

    public Action<int, int> OnTakeDamage;

    SettingsManager playerStats;
    public void Start(){
        playerStats = GetComponent<SettingsManager>();
        current_health = playerStats.settings.current_health;
        totalHealth=playerStats.settings.total_health;
        OnTakeDamage?.Invoke(playerStats.settings.current_health, playerStats.settings.total_health);

    }

    public void Awake()
    {

        
        SpriteRenderer[] allRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in allRenderers)
        {
            if (sr.gameObject.name == "Body_Sprite")
            {
                spriteRenderer = sr;
                break;
            }
        }

        if (spriteRenderer == null && allRenderers.Length > 0)
        {
            spriteRenderer = allRenderers[0];
        }
        
        if (spriteRenderer != null)
        {
            originalShader = spriteRenderer.material.shader;
            whiteShader = Shader.Find("GUI/Text Shader"); 
        }
    }

    void FixedUpdate()
    {
        if (i_frames_count >= 0)
        {
            i_frames_count -= Time.deltaTime; 
        }
    }

    public void SubtractHealth(int damage)
    {
        if (damage > 0)
        {
            if (i_frames_count <= 0) 
            {
                playerStats.settings.current_health -= (int) (damage * playerStats.settings.DamageReductionRatio); 

                OnTakeDamage?.Invoke(playerStats.settings.current_health, playerStats.settings.total_health);

                AudioManager.instance.PlaySFX(SoundType.SwordHit);
                
                if (playerStats.settings.current_health > 0)
                {
                    StartCoroutine(HitStopRoutine(0.2f));
                }
                
                i_frames_count = i_frames;
            }
            
            if (playerStats.settings.current_health <= 0)
            {   
                if (gameObject.CompareTag("Player")) 
                {
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                    current_health = 0; 
                    StartCoroutine(DeathFadeRoutine(0.5f));
                }
                
            }
        } else
        {
            playerStats.settings.current_health -= (int) (damage);
            
            if (playerStats.settings.current_health > playerStats.settings.total_health)
            {
                playerStats.settings.current_health = playerStats.settings.total_health;
            }

            OnTakeDamage?.Invoke(playerStats.settings.current_health, playerStats.settings.total_health);

            AudioManager.instance.PlaySFX(SoundType.Heal);
        }
        

        
    }

    IEnumerator HitStopRoutine(float duration)
    {
        Time.timeScale = 0f;
        if (spriteRenderer != null && whiteShader != null)
        {
            spriteRenderer.material.shader = whiteShader;
        }

        yield return new WaitForSecondsRealtime(duration); 

        if (spriteRenderer != null && originalShader != null)
        {
            spriteRenderer.material.shader = originalShader;
        }
        Time.timeScale = 1f;
    }

    IEnumerator DeathFadeRoutine(float fadeDuration)
    {
        AudioManager.instance.PlaySFX(SoundType.Kill);

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false; 
        }

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this) 
            {
                script.enabled = false;
            }
        }

        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            
            foreach (SpriteRenderer sr in renderers)
            {
                if (sr != null)
                {
                    Color c = sr.color;
                    c.a = alpha;
                    sr.color = c;
                }
            }
            
            yield return null; 
        }

        Instantiate(xp, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}