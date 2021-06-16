using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

// ReSharper disable All

/// <summary>
/// Main class that controls player
/// movement and communicates with
/// other player classes.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public Attacks at;
    public ShrinkTimer timer;
    public float maxScale; //maximum player size, minimum is assumed to be 1
    public float growthRate; //base growth rate
    public float maxMovementSpeed; //fastest speed the player can move
    public float minMovementSpeed; //slowest speed the player can move

    private float movementSpeedModifier; //intervals of movement speed decrease
    private float movementSpeed;
    private float scale; //current player scale
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        scale = gameObject.transform.localScale.x; // both scale components should be equal
        movementSpeedModifier = (maxMovementSpeed - minMovementSpeed) / (maxScale - 1);
        ScaleMovementSpeed();
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

        //movement control
        float inputY = Input.GetAxisRaw("Vertical");
        float inputX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputX * movementSpeed, inputY * movementSpeed);

        //attack control
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            at.Punch(GetCursorDirection());
        }
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
    /// Grow the player.
    /// 
    /// Called when the player destroys a building
    /// and the player is not at max size.
    /// </summary>
    /// <para>"level" level of the
    /// building the player destroyed</para>
    public void Grow(int level)
    {
        float increase = growthRate * level;

        if ((scale + increase) >= maxScale) //reach max size
        {
            scale = maxScale;
        }
        else //not at max size
        {
            scale += increase;
        }

        transform.localScale = new Vector3(scale, scale, transform.localScale.z); //scale up player
        timer.UpdateScaledTime(); //scale shrink timer
        ScaleMovementSpeed(); //scale movement speed
        at.ScaleAttackDelay(); //scale attack delay
    }

    /// <summary>
    /// Shrink the player.
    ///
    /// Called by the ShrinkTimer.
    /// </summary>
    public void Shrink()
    {
        float decrease = growthRate;

        scale -= decrease;
        transform.localScale = new Vector3(scale, scale, transform.localScale.z); // shrink player
        timer.UpdateScaledTime(); //scale shrink timer
        ScaleMovementSpeed(); //scale movement speed
        at.ScaleAttackDelay(); //scale attack delay
    }


    /// <summary>
    /// Gets a normalized two dimensional
    /// vector representing the position
    /// of the cursor relative to the player.
    /// </summary>
    /// <returns>
    /// A normalized direction vector for the mouse cursor.
    /// </returns>
    private Vector2 GetCursorDirection()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        Vector2 direction = target - position;
        direction.Normalize();

        return direction;
    }

    /// <summary>
    /// Scales the player's movement speed
    /// to their scale.
    /// </summary>
    private void ScaleMovementSpeed()
    {
        float decrease = (scale - 1) * movementSpeedModifier;
        movementSpeed = maxMovementSpeed - decrease;
    }
}
