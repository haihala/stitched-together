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

    public bool on_workbench;

    [SerializeField] private List<Limb> limbs;

    private Health health;
    private float move_direction;

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
                move_direction = 1;
                break;
            case BehaviorAction.Retreat:
                move_direction = -1;
                break;
            case BehaviorAction.Attack:
                Attack(behavior.GetTarget(this));
                move_direction = 0;
                break;
            case BehaviorAction.Special:
                move_direction = 0;
                break;
            case BehaviorAction.Wait:
                move_direction = 0;
                break;
        }
    }

    public void AttachLimb(GameObject limb)
    {
        limb.transform.parent = transform;
        limbs.Add(limb.GetComponent<Limb>());
        Rigidbody2D rb = limb.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        SetLayer(
            limb.gameObject,
            LayerMask.NameToLayer("CreatureLimbs")
        );
        SetSpringsEnabled(limb, true);

        ReCalculateProperties();
    }

    public GameObject AddLimb(GameObject limb)
    {
        GameObject new_limb = Instantiate(limb);

        AttachLimb(new_limb);

        return new_limb;
    }

    public void RemoveLimb(Limb limb)
    {
        if (limbs.Contains(limb))
        {
            limb.transform.parent = null;
            limbs.Remove(limb);
            Rigidbody2D rb = limb.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            SetLayer(
                limb.gameObject,
                LayerMask.NameToLayer("Default")
            );
            SetSpringsEnabled(limb.gameObject, false);

            ReCalculateProperties();
        }
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

    void SetLayer(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayer(child.gameObject, layer);
        }
    }

    void SetSpringsEnabled(GameObject obj, bool new_state)
    {
        foreach (SpringJoint2D spring in obj.GetComponentsInChildren<SpringJoint2D>())
        {
            spring.enabled = new_state;
        }

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
    }

    // For behaviors to make decisions
    public float GetSpeed()
    {
        return movement_force * Time.deltaTime * move_direction * Forward().x;
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

    public void Highlight()
    {

    }

    private void OnMouseEnter()
    {
        if (CreatureManager.Instance.TrySetHoveredCreature(this))
        {
            Highlight();
        }
    }

    public void Unhighlight()
    {

    }

    private void OnMouseExit()
    {
        if (CreatureManager.Instance.TryUnsetHoveredCreature(this))
        {
            Unhighlight();
        }
    }
}
