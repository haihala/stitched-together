using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangerMover : MonoBehaviour
{
    public Creature creature;
    public SpringJoint2D connector;

    void Update()
    {
        if (connector)
        {
            transform.Translate(new Vector3(creature.GetSpeed(), 0, 0));
            connector.connectedAnchor = transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
