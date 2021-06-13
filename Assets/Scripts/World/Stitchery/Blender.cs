using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    // Purpose
    //      Detect what has fallen into the blender
    //      Turn that into juice
    //      Update juice count on juice manager
    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health)
        {
            JuiceManager.Instance.Add(health.GetAmount());
            health.Die();
        }
    }
}
