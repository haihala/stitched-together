using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Purpose
    //      Keep track of the hp for a creature
    //      Do something when the hp runs out

    public float starting_health;
    private float current_health;

    void Start()
    {
        current_health = starting_health;
    }

    public float GetAmount()
    {
        return current_health;
    }

    public void Damage(float amount)
    {
        current_health -= amount;
        if (current_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // TODO needs polish
        CreatureCoordinator.Instance.RemoveCreature(GetComponent<Creature>());
        Destroy(gameObject);
    }
}
