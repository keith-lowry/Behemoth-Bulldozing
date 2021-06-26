using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Timer that scales in duration
/// with the player's scale.
/// </summary>
public class ScalingTimer : MonoBehaviour
{
    public PlayerController pc;
    public float maxTime;
    public float minTime;

    private float timeModifier; //increments for scaling the timer's duration
    private float time; //the current duration of the timer
    protected float timeLeft; //amount of time left in timer

    // Start is called before the first frame update
    void Start()
    {
        ScaleTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void Reset()
    {

    }

    /// <summary>
    /// Scales the Timer to fit 
    /// </summary>
    public void ScaleTimer()
    {

    }

    /// <summary>
    /// Decreases the time left in
    /// the timer.
    ///
    /// Sets time left to 0 when time
    /// has run out.
    /// </summary>
    protected void TikTok()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                timeLeft = 0;
            }
        }
    }

    


}
