using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Audio;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

// ReSharper disable All

/// <summary>
/// Player controller script that handles
/// player movement and communicates with
/// other player classes.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float maxScale = 5f; //maximum player size
    public float minScale = 1f; //minimum player size
    public float growthRate = 0.1f; //base growth rate
    public float maxMovementSpeed = 0.9f; //fastest speed the player can move
    public float minMovementSpeed = 0.3f; //slowest speed the player can move


    private Attacks at;
    private ShrinkTimer shrinkTimer;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float movementSpeedModifier; //intervals of movement speed decrease
    private float movementSpeed; //movement speed scaled to player's current size
    private float inputX;
    private float inputY;
    private float scale; //current player scale

    //Awake is called when the script instance is being loaded
    //called before Start()
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        at = GetComponent<Attacks>();
        shrinkTimer = GetComponent<ShrinkTimer>();

        scale = minScale; //Player is initialized to minScale by default
        movementSpeedModifier = (maxMovementSpeed - minMovementSpeed) / (maxScale - minScale); // calculate "steps" btw max and min movement speed

        Scale();
    }

    // Update is called once per frame
    void Update()
    {
        //transform control
        Vector3 relativePos = GetCursorDirection();
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg; // Get angle of cursor, convert to degrees
        angle -= 90f;                                                            // North is considered the front, subtract 90 degrees
        angle = (float) Math.Round(angle, 1);
        Quaternion rotation = UnityEngine.Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        //animator control

        //gather movement inputs
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
    }

    //FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
        //movement control
        rb.velocity = new Vector2(inputX * movementSpeed, inputY * movementSpeed);
    }

    /// <summary>
    /// Get the player's current
    /// scale.
    /// </summary>
    /// <returns>
    /// Player's current scale.
    /// </returns>
    public float GetScale()
    {
        return scale;
    }

    /// <summary>
    /// Gets the player's ShrinkTimer
    /// component.
    /// </summary>
    /// <returns>
    /// The player's ShrinkTimer
    /// component.
    /// </returns>
    public ShrinkTimer GetShrinkTimer()
    {
        return shrinkTimer;
    }

    /// <summary>
    /// Grow the player based on the scale
    /// of the building destroyed.
    /// 
    /// Called when the player destroys a building.
    /// </summary>
    /// <param name="buildingScale">
    /// Scale of the
    /// building the player destroyed.
    /// </param>
    public void Grow(float buildingScale)
    {

        float increase = growthRate * buildingScale;

        if ((scale + increase) >= maxScale) //would reach max size
        {
            scale = maxScale;
        }
        else //would not reach max size
        {
            scale += increase;
        }

        //Scale player and components
        Scale();
        shrinkTimer.Scale();
        at.Scale(); 
    }

    /// <summary>
    /// Shrinks the player if it
    /// is not at minimum scale
    /// already.
    ///
    /// Called by the ShrinkTimer.
    /// </summary>
    public void Shrink()
    {
        float decrease = growthRate;

        if ((scale - decrease) <= minScale) //would reach min size
        {
            scale = minScale;
        }
        else //would not reach min size
        {
            scale -= decrease;
        }

        //Scale player and components
        Scale();
        shrinkTimer.Scale();
        at.Scale(); 
    }

    /// <summary>
    /// Moves the player transform forward
    /// in the direction of the cursor.
    /// </summary>
    public void Dash()
    {
        //TODO: implement Dash
    }

    /// <summary>
    /// Gets a normalized two dimensional
    /// vector representing the position
    /// of the cursor relative to the player.
    /// </summary>
    /// <returns>
    /// A normalized direction vector for the mouse cursor.
    /// </returns>
    public Vector2 GetCursorDirection()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        Vector2 cursorDirection = cursorPosition - playerPosition;
        cursorDirection.Normalize();

        return cursorDirection;
    }

    /// <summary>
    /// Checks if the player is at
    /// the maximum scale.
    /// </summary>
    /// <returns>
    /// True if the player is at maximum
    /// scale, false otherwise.
    /// </returns>
    public bool AtMaximumScale()
    {
        return (scale == maxScale);
    }

    /// <summary>
    /// Checks if the player is at
    /// the minimum scale.
    /// </summary>
    /// <returns>
    /// True if the player is at minimum
    /// scale, false otherwise.
    /// </returns>
    public bool AtMinimumScale()
    {
        return (scale == minScale);
    }

    /// <summary>
    /// Scales the player's movement speed
    /// and size to their current scale.
    /// </summary>
    private void Scale()
    {
        float movementSpeedDecrease = (scale - minScale) * movementSpeedModifier;
        movementSpeed = maxMovementSpeed - movementSpeedDecrease;

        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
    }
}
