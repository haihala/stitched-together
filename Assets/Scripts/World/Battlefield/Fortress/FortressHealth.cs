using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressHealth : MonoBehaviour
{
    // Purpose
    //      Track base health
    //      Go to defeat screen on a loss
    public float starting_health;

    private SceneChanger to_defeat_screen;
    private float current_health;

    // Start is called before the first frame update
    void Start()
    {
        to_defeat_screen = GetComponent<SceneChanger>();
        current_health = starting_health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Creature creature = other.GetComponent<Creature>();
        if (creature != null)
        {
            if (creature.team == Team.Enemy)
            {
                Health health = other.GetComponent<Health>();
                Damage(health.GetAmount());
                health.Die();
            }
        }
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
        to_defeat_screen.Load();
    }
}
