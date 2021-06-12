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
    private float z = -10;

    public void JumpToStitchery()
    {
        transform.position = new Vector3(stitchery.position.x, stitchery.position.y, z);
    }
    public void JumpToBattlefield()
    {
        transform.position = fortress.position + battlefield_offset;
    }
}
