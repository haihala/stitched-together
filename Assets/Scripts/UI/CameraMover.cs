using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // Purpose:
    //      Move camera between views
    //      Move camera when edge panning
    public Transform stitchery;
    public Transform fortress;
    private Vector3 battlefield_offset;

    public void JumpToStitchery()
    {
        transform.position = stitchery.position;
    }
    public void JumpToBattlefield()
    {
        transform.position = fortress.position + battlefield_offset;
    }
}
