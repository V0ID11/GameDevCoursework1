using UnityEngine;
using System.Collections;

public class MinotaurAbility : MonoBehaviour
{

    private Bounce bounceScript;
    private int normalVelocity;

    void Start()
    {
        bounceScript = GetComponent<Bounce>();

        if (bounceScript != null)
        {
            normalVelocity = bounceScript.totalVelocity;
        }


        StartCoroutine(SpeedBoostRoutine());
    }

    private IEnumerator SpeedBoostRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            AudioManager.instance.PlaySFX(SoundType.BossAbility);

            yield return new WaitForSeconds(0.5f);

            if (bounceScript != null) bounceScript.totalVelocity = normalVelocity * 2;
            
            yield return new WaitForSeconds(2f);  
            
            if (bounceScript != null) bounceScript.totalVelocity = normalVelocity;
        }
    }
}