using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public Transform target;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Creature>())
        {
            other.transform.position = target.position;
        }
    }
}
