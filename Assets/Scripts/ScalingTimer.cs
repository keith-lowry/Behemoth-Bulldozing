using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Timer that scales in duration
/// with the player's scale.
///
/// The Timer gets shorter as the
/// player increases in scale until
/// it reaches maxScale.
/// </summary>
public class ScalingTimer : MonoBehaviour
{
    public PlayerController pc;
    public float maxTime = 10f; //max duration of timer
    public float minTime = 3f;  //min duration of timer

    private float timeModifier; //increments for scaling the timer's duration
    private float time; //the current duration of the timer
    protected float timeLeft; //running time left in timer

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        TikTok();
    }

    /// <summary>
    /// Decreases the time left in
    /// the Timer.
    ///
    /// Sets time left to 0 when time
    /// has run out.
    /// </summary>
    protected void TikTok()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0f)
            {
                timeLeft = 0f;
            }
        }
    }

    /// <summary>
    /// Initializes the timer with a timeModifier
    /// and scales its duration to match the player's
    /// scale. Should be called in Awake().
    /// </summary>
    protected void SetUp()
    {
        timeModifier = (maxTime - minTime) / (pc.maxScale - pc.minScale);
        time = maxTime; // IMPORTANT: does not call ScaleTimer() when initialized
                        // Assumption that player begins game at minScale
    }

    /// <summary>
    /// Resets the Timer.
    /// </summary>
    public void Reset()
    {
        timeLeft = time;
    }

    /// <summary>
    /// Scales the Timer's duration to
    /// the player's current scale.
    /// </summary>
    public void ScaleTimer()
    {
        float decrease = (pc.GetScale() - pc.minScale) * timeModifier;
        time = maxTime - decrease;
    }
}
