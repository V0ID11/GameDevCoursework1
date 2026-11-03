using UnityEngine;
using System.Collections;

public class MinotaurAbility : MonoBehaviour
{
    private float normalDuration = 8f;
    private float boostDuration = 2f;

    void Start()
    {
        StartCoroutine(SpeedBoostRoutine());
    }

    private IEnumerator SpeedBoostRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(normalDuration);

            // 2. DOUBLE THE SPEED

            yield return new WaitForSeconds(boostDuration);

            // 4. REVERT THE SPEED
        }
    }
}