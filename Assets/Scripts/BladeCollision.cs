using UnityEngine;

public class BladeCollision : MonoBehaviour
{

    Rotate parent_rotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent_rotation = gameObject.GetComponentInParent<Rotate>();
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.transform.IsChildOf(transform.parent)) return;
        if (collision.gameObject.tag == "Arrow") return;

        AudioManager.instance.PlaySFX(SoundType.Parry);
        parent_rotation.RotationSpeed = -1 * parent_rotation.RotationSpeed;
    }
    
}
