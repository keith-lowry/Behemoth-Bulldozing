using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCamera : MonoBehaviour
{
    public float speed; // horizontal speed of the camera
    public float delay;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("UpdateVelocity", delay); //start moving after delay
    }

    /// <summary>
    /// Sets the cameras velocity.
    /// </summary>
    private void UpdateVelocity()
    {
        rb.velocity = new Vector2(speed, 0);
    }
    
}
