using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverDetector : MonoBehaviour
{
    public bool hovered = false;

    public void OnMouseEnter()
    {
        hovered = true;
    }

    public void OnMouseExit()
    {
        hovered = false;
    }
}
