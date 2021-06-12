using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureFactory : MonoBehaviour
{
    public static GameObject Create(GameObject body, GameObject limb)
    {
        List<GameObject> limbs = new List<GameObject>();
        limbs.Add(limb);
        return Create(body, limbs);
    }

    public static GameObject Create(GameObject body, List<GameObject> limbs)
    {
        GameObject instance = Instantiate(body);
        Torso torso = instance.GetComponent<Torso>();
        Creature creature = instance.GetComponent<Creature>();
        foreach (Transform spot in torso.reasonable_limb_spots)
        {
            creature.AddLimb(limbs[Random.Range(0, limbs.Count)], spot);
        }
        return instance;
    }
}
