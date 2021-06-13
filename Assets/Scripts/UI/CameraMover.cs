using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Transform pan_floor;

    private float battlefield_offset;
    private bool in_stitchery = false;
    private float z = -10;

    private void Start()
    {
        JumpToStitchery();
    }

    public void JumpToStitchery()
    {
        battlefield_offset = transform.position.x;
        transform.position = new Vector3(stitchery.position.x, stitchery.position.y, z);
        in_stitchery = true;
        left_camera_panner.GetComponent<Image>().enabled = false;
        right_camera_panner.GetComponent<Image>().enabled = false;

    }
    public void JumpToBattlefield()
    {
        transform.position = new Vector3(
            fortress.position.x + battlefield_offset,
            fortress.position.y,
            z);
        in_stitchery = false;
        left_camera_panner.GetComponent<Image>().enabled = true;
        right_camera_panner.GetComponent<Image>().enabled = true;
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
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, pan_floor.position.x - pan_floor.localScale.x / 2, pan_floor.position.x + pan_floor.localScale.x / 2);
            transform.position = clampedPosition;
        }
    }
}
