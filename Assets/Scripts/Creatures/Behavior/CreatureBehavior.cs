using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureBehavior : ScriptableObject
{
    // Purpose
    //      Base class for different types of creatures decision making styles

    public abstract BehaviorAction GetAction(Creature self);
}


public enum BehaviorAction
{
    Advance,
    Retreat,
    Attack,
    Special,
    Wait,
};