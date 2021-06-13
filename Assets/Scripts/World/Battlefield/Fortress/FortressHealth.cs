using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortressHealth : MonoBehaviour
{
    // Purpose
    //      Track base health
    //      Go to defeat screen on a loss
    public float starting_health;
    public Slider display;

    private SceneChanger to_defeat_screen;
    private float current_health;

    // Start is called before the first frame update
    void Start()
    {
        to_defeat_screen = GetComponent<SceneChanger>();
        current_health = starting_health;
        display.maxValue = starting_health;
    }

    private void Update()
    {
        display.value = current_health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = FindHealth(other.gameObject);
        if (health)
        {
            Debug.Log(health.GetAmount());
            Damage(health.GetAmount());
            health.Die();
        }
    }

    Health FindHealth(GameObject target)
    {
        Creature creature = target.GetComponent<Creature>();
        if (creature)
        {
            if (creature.team == Team.Enemy)
            {
                Health health = target.GetComponent<Health>();
                return health;
            }
        }
        else if (target.transform.parent)
        {
            return FindHealth(target.transform.parent.gameObject);
        }
        return null;
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
