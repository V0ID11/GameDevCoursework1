using UnityEngine;
using System.Collections;

public class KnightAbility : MonoBehaviour
{
    private Rotate rotateScript;
    private float normalRotate;

    void Start()
    {
        rotateScript = GetComponentInChildren<Rotate>();
        

        if (rotateScript != null)
        {
            normalRotate = rotateScript.RotationSpeed;
        }

        StartCoroutine(SpeedBoostRoutine());
    }

    private IEnumerator SpeedBoostRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(8f);

            AudioManager.instance.PlaySFX(SoundType.BossAbility);

            yield return new WaitForSeconds(0.5f);

            if (rotateScript != null) rotateScript.RotationSpeed = normalRotate * 3;
            
            yield return new WaitForSeconds(3f);  
            
            if (rotateScript != null) rotateScript.RotationSpeed = normalRotate;
        }
    }
}