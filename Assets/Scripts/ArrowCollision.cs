using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D collision){
        Destroy(gameObject);
     }
}
