using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCoordinator : MonoBehaviour
{
    // Purpose
    //      Jump between views (Battlefield and Stitchery)
    //      Tell CameraMover to move when jumping between views
    //      Store information on which view we are in for other components
    //      Play sfx etc when transitioning

    new public CameraMover camera;

    private bool in_stitchery = true;

    public bool InStitchery()
    {
        return in_stitchery;
    }

    public void JumpToStitchery()
    {
        in_stitchery = true;
        camera.JumpToStitchery();
    }

    public bool InBattlefield()
    {
        return !in_stitchery;
    }

    public void JumpToBattlefield()
    {
        in_stitchery = false;
        camera.JumpToBattlefield();
    }
}
