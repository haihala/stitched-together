using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    // Purpose
    //      Store mechanics numbers of the limb
    //      Store the type of limb (Hand, Leg, Other?)
    [Tooltip("Health lost per second for having one of these")]
    public float decay_speed;
    public float size;     // Attack range?
    public float speed;     // Movement and attack speed?
    public float power;     // Attack damage
}
