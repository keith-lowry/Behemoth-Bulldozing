using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Timer that scales in duration with the player's scale.
///
/// The Timer scales up or down in duration with the player's
/// scale according to the chosen ScalingEnum. All classes
/// that inherit from this script must call SetUp() in
/// Start() after initializing the ScalingEnum field.
/// </summary>
public class ScalingTimer : MonoBehaviour
{
    public float maxTime; //max duration of timer
    public float minTime;  //min duration of timer
    public ScalingEnum scaleTimer;

    protected PlayerController pc;
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
    /// Initializes the timer's PlayerController component
    /// and timeModifier. Scales the timer's duration to the
    /// player's beginning scale.
    ///
    /// Must be called in Start().
    /// </summary>
    protected void SetUp()
    {
        pc = GetComponent<PlayerController>();
        timeModifier = (maxTime - minTime) / (pc.maxScale - pc.minScale);
        Scale();
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
    ///
    /// Follows the scaling rule chosen
    /// by the ScalingEnum.
    /// </summary>
    public void Scale()
    {
        Invoke(scaleTimer.ToString(), 0f);
    }
    
    /// <summary>
    /// Scale the timer's duration up
    /// with player scale.
    /// </summary>
    private void ScaleUp()
    {
        float increase = (pc.GetScale() - pc.minScale) * timeModifier;
        time = minTime + increase;
    }

    /// <summary>
    /// Scale the timer's duration down
    /// with player scale.
    /// </summary>
    private void ScaleDown()
    {
        float decrease = (pc.GetScale() - pc.minScale) * timeModifier;
        time = maxTime - decrease;
    }
}

/// <summary>
/// Enum determining whether the timer
/// scales up or down in duration as
/// the player's scale increases.
/// </summary>
public enum ScalingEnum
{
    ScaleUp, ScaleDown
}
