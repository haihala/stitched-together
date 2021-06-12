using System;
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

    public HoverDetector left_camera_panner;
    public HoverDetector right_camera_panner;

    public float pan_speed;
    public float max_pan_distance;

    private float battlefield_offset;
    private bool in_stitchery = false;
    private float z = -10;

    public void JumpToStitchery()
    {
        battlefield_offset = transform.position.x;
        transform.position = new Vector3(stitchery.position.x, stitchery.position.y, z);

    }
    public void JumpToBattlefield()
    {
        transform.position = new Vector3(
            fortress.position.x + battlefield_offset,
            fortress.position.y,
            z);
    }

    private void Update()
    {
        if (!in_stitchery)
        {
            // Positive if panning to the right, negative if to the left.
            float pan_direction = (float)Convert.ToInt32(right_camera_panner.hovered) - (float)Convert.ToInt32(left_camera_panner.hovered);
            float pan_shift = pan_direction * pan_speed * Time.deltaTime;
            transform.Translate(new Vector3(pan_shift, 0, 0));

            // Clamp
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, 0, max_pan_distance);
            transform.position = clampedPosition;
        }
    }
}
