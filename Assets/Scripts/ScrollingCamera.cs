using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that makes the camera move horizontally
/// at a constant speed after a given delay.
///
/// Camera must have a rigidbody attached.
/// </summary>
public class ScrollingCamera : MonoBehaviour
{
    public float speed = 2f; // horizontal speed of the camera
    public float delay = 3f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("UpdateVelocity", delay); //start moving after delay
    }

    /// <summary>
    /// Sets the camera's velocity.
    /// </summary>
    private void UpdateVelocity()
    {
        rb.velocity = new Vector2(speed, 0);
    }
    
}
