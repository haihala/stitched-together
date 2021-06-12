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
    public GameObject test_limb;

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
                // print("Advance!");
                rigidbody.AddForce(Forward() * movement_force);
                break;
            case BehaviorAction.Retreat:
                // print("Retreat!");
                rigidbody.AddForce(Backward() * movement_force);
                break;
            case BehaviorAction.Attack:
                // print("Attack!");
                break;
            case BehaviorAction.Special:
                // print("Special!");
                break;
            case BehaviorAction.Wait:
                // print("Wait!");
                break;
            default:
                Debug.LogError("Unlisted action");
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

    private void OnMouseDown()
    {
        Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Camera is at z=-10 and not zeroing this makes things invisible but only in the game not in editor
        // Joo oli hauska bugi track down.
        mouse_position.z = 0;
        AddLimb(test_limb, mouse_position);
    }
}
