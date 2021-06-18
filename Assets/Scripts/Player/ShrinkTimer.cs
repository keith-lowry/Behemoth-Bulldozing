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

        //shrinks player if time is 0 and delay between shrinks has elapsed
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
    /// Checks whether the player can be shrunk
    /// or not.
    /// </summary>
    /// <returns>
    /// True when the time left on
    /// the timer is 0 and the delay
    /// between shrinks
    /// has elapsed, false otherwise.
    /// </returns>
    private bool CanShrink()
    {
        return (timeLeft == 0 && Time.time >= nextShrink);
    }

}
