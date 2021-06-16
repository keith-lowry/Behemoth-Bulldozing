using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
/// <summary>
/// Simple timer that keeps track of when a
/// player should begin to shrink.
/// </summary>
public class ShrinkTimer : Timer
{
    public PlayerController pc;
    public float shrinkDelay; //delay between shrinks

    private float scaledTime; //scaled time in seconds before
                              //player starts to shrink
    private float nextShrink; //time of next shrink in seconds

    // Start is called before the first frame update
    void Start()
    {
        scaledTime = baseTime;
        timeLeft = scaledTime;
        nextShrink = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        TikTok(); //decreases timeLeft

        //shrinks player if time is 0 and player is not already at base scale
        if (CanShrink())
        {
            pc.Shrink();
            nextShrink = Time.time + shrinkDelay;
        }

    }

    /// <summary>
    /// Resets the timer to
    /// scaledTime.
    /// </summary>
    public new void Reset()
    {
        timeLeft = scaledTime;
    }

    /// <summary>
    /// Updates the scaled time based
    /// on the player's scale.
    /// </summary>
    public void UpdateScaledTime()
    {
        scaledTime = baseTime / pc.GetScale();
    }

    /// <summary>
    /// Checks whether the player can be shrunk.
    ///
    /// Returns true when the time left on
    /// the timer is 0, the player is not at
    /// a scale of 1, and the delay between shrinks
    /// has elapsed.
    /// </summary>
    /// <returns>
    /// Can the player be shrunk?
    /// </returns>
    private bool CanShrink()
    {
        return (timeLeft == 0 && transform.localScale != Vector3.one && Time.time >= nextShrink);
    }

}
