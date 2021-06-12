using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    // Purpose
    //      Store information on the creature's parts
    //      Store and use the behavior class of the creature
    //      Manage a single creature

    public CreatureBehavior behavior;
    public Team team;

    [SerializeField] private List<Limb> limbs;

    private Health health;

    private float decay_amount;
    private float movement_force;
    private float next_legal_attack;
    private float attack_delay;
    private float attack_range;

    new private Rigidbody2D rigidbody;

    void Start()
    {
        health = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        health.Damage(decay_amount);
        switch (behavior.GetAction(this))
        {
            case BehaviorAction.Advance:
                print("Advance!");
                rigidbody.AddForce(Forward() * movement_force);
                break;
            case BehaviorAction.Retreat:
                print("Retreat!");
                rigidbody.AddForce(Backward() * movement_force);
                break;
            case BehaviorAction.Attack:
                print("Attack!");
                break;
            case BehaviorAction.Special:
                print("Special!");
                break;
            case BehaviorAction.Wait:
                print("Wait!");
                break;
            default:
                Debug.LogError("Unlisted action");
                break;
        }
    }

    public void AddLimb(GameObject limb, Vector3 position)
    {
        Instantiate(
            limb,
            position,
            limb.transform.rotation,
            transform
            );

        limbs.Add(limb.GetComponent<Limb>());
        ReCalculateProperties();
    }

    void ReCalculateProperties()
    {
        // TODO update decay_amount, speed and co.
    }

    // For behaviors to make decisions
    public float GetSpeed()
    {
        return movement_force;
    }

    public bool CanAttack()
    {
        return Time.time > next_legal_attack;
    }
    public bool EnemiesInRange()
    {
        foreach (Creature opponent in CreatureCoordinator.Instance.GetOpponents(team))
        {
            float distance = (opponent.transform.position - transform.position).magnitude;
            if (distance < attack_range)
            {
                return true;
            }
        }
        return false;
    }

    public Vector3 Forward()
    {
        if (team == Team.Player)
        {
            return new Vector3(1, 0, 0);
        }
        else
        {
            return new Vector3(-1, 0, 0);
        }
    }

    public Vector3 Backward()
    {
        return -Forward();
    }
}
