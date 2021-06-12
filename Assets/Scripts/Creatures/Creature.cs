using System.Collections;
using System.Collections.Generic;
using System;

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

    private float next_legal_attack = 0;
    private float decay_speed;
    private float movement_force;
    private float attack_delay;
    private float attack_range;
    private float attack_damage;

    new private Rigidbody2D rigidbody;

    void Start()
    {
        health = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        health.Damage(decay_speed * Time.deltaTime);
        switch (behavior.GetAction(this))
        {
            case BehaviorAction.Advance:
                // print("Advance!");
                Vector2 force = (Vector2)(Forward() * movement_force);
                print(force);
                rigidbody.AddForce(force, ForceMode2D.Force);
                // print(rigidbody.);
                break;
            case BehaviorAction.Retreat:
                // print("Retreat!");
                // rigidbody.AddForce(Backward() * movement_force);
                break;
            case BehaviorAction.Attack:
                // print("Attack!");
                Attack(behavior.GetTarget(this));
                break;
            case BehaviorAction.Special:
                // print("Special!");
                break;
            case BehaviorAction.Wait:
                // print("Wait!");
                break;
        }
    }

    public GameObject AddLimb(GameObject limb)
    {
        GameObject new_limb = Instantiate(
            limb,
            transform
            );

        Collider2D[] children_colliders = new_limb.GetComponentsInChildren<Collider2D>();
        Collider2D body_collider = GetComponent<Collider2D>();

        foreach (Collider2D collider in children_colliders)
        {
            Physics2D.IgnoreCollision(collider, body_collider);
            foreach (Collider2D collider2 in children_colliders)
            {
                Physics2D.IgnoreCollision(collider, collider2);
            }

        }

        limbs.Add(new_limb.GetComponent<Limb>());
        ReCalculateProperties();
        return new_limb;
    }
    public GameObject AddLimb(GameObject limb, Vector3 position)
    {
        GameObject new_limb = AddLimb(limb);
        new_limb.transform.position = position;

        Vector3 direction_vector = position - transform.position;
        float angle = Mathf.Atan2(direction_vector.x, direction_vector.y) * Mathf.Rad2Deg;
        new_limb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        return new_limb;
    }
    public GameObject AddLimb(GameObject limb, Transform target_transform)
    {
        GameObject new_limb = AddLimb(limb);
        new_limb.transform.position = target_transform.position;
        new_limb.transform.rotation = target_transform.rotation;
        return new_limb;
    }

    void ReCalculateProperties()
    {
        decay_speed = 0;
        movement_force = 0;
        attack_delay = 1;
        attack_range = 1;
        attack_damage = 0;
        foreach (Limb limb in limbs)
        {
            decay_speed += limb.decay_speed;
            attack_damage += limb.power;
            attack_range += limb.size;
            movement_force += limb.speed;

            attack_delay /= limb.speed;
        }

        movement_force /= 100;
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
    public List<Creature> EnemiesInRange()
    {
        List<Creature> collector = new List<Creature>();
        foreach (Creature opponent in CreatureCoordinator.Instance.GetOpponents(team))
        {
            float distance = (opponent.transform.position - transform.position).magnitude;
            if (distance < attack_range)
            {
                collector.Add(opponent);
            }
        }
        return collector;
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

    private void Attack(Creature target)
    {
        if (!CanAttack())
        {
            throw new Exception("Attacking but CanAttack is false");
        }
        target.GetComponent<Health>().Damage(attack_damage);
        next_legal_attack = Time.time + attack_delay;
    }
}
