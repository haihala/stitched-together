using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoBehavior", menuName = "Stitched together/NoBehavior", order = 0)]
public class NoBehavior : CreatureBehavior
{
    public override BehaviorAction GetAction(Creature self)
    {
        return BehaviorAction.Wait;
    }

}
