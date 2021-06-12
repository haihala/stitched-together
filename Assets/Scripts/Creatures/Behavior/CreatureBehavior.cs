using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureBehavior : ScriptableObject
{
    // Purpose
    //      Base class for different types of creatures decision making styles

    public abstract BehaviorAction GetAction(Creature self);
    public Creature GetTarget(Creature self)
    {
        float shortest_distance = Mathf.Infinity;
        Creature closest = null;
        foreach (Creature possible_target in self.EnemiesInRange())
        {
            float distance = (possible_target.transform.position - self.transform.position).magnitude;
            if (closest == null || distance < shortest_distance)
            {
                closest = possible_target;
                shortest_distance = distance;
            }
        }
        return closest;
    }
}


public enum BehaviorAction
{
    Advance,
    Retreat,
    Attack,
    Special,
    Wait,
};