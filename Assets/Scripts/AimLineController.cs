using UnityEngine;

public class AimLineController : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lineRenderer;
    public Transform firePoint; 

    [Header("Settings")]
    public float maxAimDistance = 15f;
    public LayerMask hitLayers; 
    public bool isAiming = true; 

    void Update()
    {
        if (lineRenderer == null || firePoint == null) return;

        
        lineRenderer.enabled = isAiming;
        if (!isAiming) return;

        
        Vector2 origin = firePoint.position;
        Vector2 direction = firePoint.right; 

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxAimDistance, hitLayers);

        
        lineRenderer.SetPosition(0, origin);

        if (hit.collider != null)
        {
           
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            
            Vector2 endPoint = origin + (direction * maxAimDistance);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}