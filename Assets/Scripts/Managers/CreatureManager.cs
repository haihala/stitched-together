using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    // Purpose:
    //      Remove body parts
    //      Attach body parts
    //      Move Creatures in stitchery (Should be in this script because it is the easy way to resolve if a click hits a joint and the body, limb moves.)
    private Creature hovered_creature;
    private LimbJoint hovered_limb;

    private static CreatureManager _instance;
    public static CreatureManager Instance { get { return _instance; } }
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

    public bool TrySetHoveredLimb(LimbJoint limb)
    {
        if (hovered_limb)
        {
            return false;
        }
        hovered_limb = limb;
        return true;
    }

    public bool TryUnsetHoveredLimb(LimbJoint limb)
    {
        if (hovered_limb == limb)
        {
            hovered_limb = null;
            return true;
        }
        return false;
    }
}
