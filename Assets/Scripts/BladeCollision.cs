using UnityEngine;

public class BladeCollision : MonoBehaviour
{

    Rotate parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = gameObject.GetComponentInParent<Rotate>();
    }

    void OnCollisionEnter2D(Collision2D collision){
        parent.RotationSpeed = -1 * parent.RotationSpeed;
    }
    
}
