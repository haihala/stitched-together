using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BrainlessBehavior", menuName = "Stitched together/BrainlessBehavior", order = 0)]
public class BrainlessBehavior : CreatureBehavior
{
    public override BehaviorAction GetAction(Creature self)
    {
        bool target_in_range = self.EnemiesInRange().Count != 0;
        if (target_in_range && self.CanAttack())
        {
            return BehaviorAction.Attack;
        }
        return BehaviorAction.Advance;
    }
}
