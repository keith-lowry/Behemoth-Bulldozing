using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

/// <summary>
/// ScalingTimer that tells the player to
/// shrink when time runs out and certain
/// conditions are fulfilled.
/// </summary>
public class ShrinkTimer : ScalingTimer
{
    public float shrinkDelay = 0.2f; //delay between shrinks

    private float nextShrink; //time of next shrink in seconds

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
        
        nextShrink = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        TikTok(); //decreases timeLeft

        if (CanShrink())
        {
            pc.Shrink();
            nextShrink = Time.time + shrinkDelay;
        }

    }

    /// <summary>
    /// Checks whether the player can be shrunk
    /// or not.
    /// </summary>
    /// <returns>
    /// True when the time left on
    /// the timer is 0, the delay
    /// between shrinks has elapsed,
    /// and player is not at min scale,
    /// false otherwise.
    /// </returns>
    private bool CanShrink()
    {
        return (timeLeft == 0 && Time.time >= nextShrink && !pc.AtMinimumScale());
    }

}
