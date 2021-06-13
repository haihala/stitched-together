using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    // Purpose:
    //      Remove body parts
    //      Attach body parts
    //      Move Creatures in stitchery (Should be in this script because it is the easy way to resolve if a click hits a joint and the body, limb moves.)
    public Zone work_area;

    public Creature hovered_creature;
    public LimbJoint hovered_limb;

    public GameObject drag_target;
    private Vector3 drag_point;
    private Vector3 pointer_position;

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
    public bool TrySetHoveredCreature(Creature creature)
    {
        if (hovered_creature)
        {
            return false;
        }
        hovered_creature = creature;
        return true;
    }

    public bool TryUnsetHoveredCreature(Creature creature)
    {
        if (hovered_creature == creature)
        {
            hovered_creature = null;
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hovered_limb)
            {
                if (hovered_creature)
                {
                    if (work_area.Contains(hovered_creature.gameObject))
                    {
                        hovered_creature.RemoveLimb(hovered_limb.GetComponent<Limb>());
                        StartDrag(hovered_limb.gameObject);
                    }
                    else
                    {
                        StartDrag(hovered_creature.gameObject);
                    }
                }
                else
                {
                    StartDrag(hovered_limb.gameObject);
                }
            }
            else if (hovered_creature)
            {
                StartDrag(hovered_creature.gameObject);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (drag_target)
                EndDrag();
        }

        Vector3 new_pointer_position = PointingAt();

        if (drag_target)
        {
            Vector3 shift = new_pointer_position - pointer_position;
            drag_target.transform.position += shift;
        }

        pointer_position = new_pointer_position;
    }

    void StartDrag(GameObject target)
    {
        drag_target = target;

        SetColliderStateRecursively(drag_target, false);
        drag_target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    void EndDrag()
    {
        SetColliderStateRecursively(drag_target, true);
        drag_target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        drag_target = null;
    }

    void SetColliderStateRecursively(GameObject obj, bool collide)
    {
        Collider2D col = obj.GetComponent<Collider2D>();
        if (col)
        {
            col.enabled = collide;
        }

        foreach (Transform child in obj.transform)
        {
            SetColliderStateRecursively(child.gameObject, collide);
        }
    }

    Vector3 PointingAt()
    {
        // Where mouse is pointing at in world space
        Vector3 original_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(original_point.x, original_point.y, 0);
    }
}
