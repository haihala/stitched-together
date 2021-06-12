using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbJoint : MonoBehaviour
{
    // Purpose:
    //      Spring wiggle body parts
    //      Highlight on hover

    // Start is called before the first frame update
    public GameObject highlighter;

    private void Start()
    {
        Unhighlight();
    }

    public void Highlight()
    {
        highlighter.SetActive(true);
    }

    public void Unhighlight()
    {
        highlighter.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (CreatureManager.Instance.TrySetHoveredLimb(this))
        {
            Highlight();
        }
    }

    private void OnMouseExit()
    {
        if (CreatureManager.Instance.TryUnsetHoveredLimb(this))
        {
            Unhighlight();
        }
    }
}
