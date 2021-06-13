using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceManager : MonoBehaviour
{
    // Purpose
    //      Store how much juice the player has
    //      Use that juice to do stuff with it
    public float unit_juice_cost;
    float juice_available;

    private static JuiceManager _instance;
    public static JuiceManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void Add(float amount)
    {
        juice_available += amount;
        if (juice_available > unit_juice_cost)
        {
            CreatureCoordinator.Instance.CreateFodderCreature();
            juice_available -= unit_juice_cost;
        }
    }
}
