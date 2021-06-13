using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public Transform target;
    public CreatureBehavior behavior;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Creature>())
        {
            other.transform.position = target.position;
            // Make a new hanger
            other.GetComponent<HangerSpawner>().Spawn();
            other.GetComponent<SpringJoint2D>().enabled = true;
            Creature creature = other.GetComponent<Creature>();
            creature.behavior = behavior;
            creature.team = Team.Player;
        }
        else if (other.transform.parent == null && other.GetComponent<Limb>())
        {
            Destroy(other.gameObject);
        }
    }
}
