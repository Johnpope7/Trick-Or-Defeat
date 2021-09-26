using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnabler : MonoBehaviour
{
    //all this does is turn on enemy colliders after they enter the level so they cant leave
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Enemy"))
        {
            GameObject otherObject = _other.gameObject;
            BoxCollider2D[] colliders = otherObject.GetComponents<BoxCollider2D>();
            for (int i = 0; i < colliders.Length; i++) 
            {
                colliders[i].enabled = true;
            }

        }
        
    }
}
