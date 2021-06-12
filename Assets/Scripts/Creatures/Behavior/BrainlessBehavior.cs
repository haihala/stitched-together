using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BrainlessBehavior", menuName = "Stitched together/BrainlessBehavior", order = 0)]
public class BrainlessBehavior : CreatureBehavior
{
    public override BehaviorAction GetAction(Creature self)
    {
        if (self.EnemiesInRange() && self.CanAttack())
        {
            return BehaviorAction.Attack;
        }
        return BehaviorAction.Advance;
    }
}
