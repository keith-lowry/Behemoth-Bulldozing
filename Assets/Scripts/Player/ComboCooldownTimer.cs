using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Timer that represents the cooldown
/// present in between a combo being completed
/// and a new one beginning.
///
/// Timer scales up in duration as the player
/// increases in size.
/// </summary>
public class ComboCooldownTimer : ScalingTimer
{

    // Start is called before the first frame update
    void Start()
    {
        scaleTimer = ScalingEnum.ScaleUp;

        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        TikTok();
    }

    /// <summary>
    /// Checks whether a combo can be made.
    ///
    /// A combo can be made when the timer hits 0.
    /// </summary>
    /// <returns>
    /// True if a combo can be made, false otherwise.
    /// </returns>
    public bool CanCombo()
    {
        return timeLeft == 0f;
    }
}
