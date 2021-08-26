using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple timer that counts down to 0 from
/// a determined base time and can be reset.
/// </summary>
public class BasicTimer : MonoBehaviour
{
    public float baseTime;

    protected float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = baseTime;
    }

    // Update is called once per frame
    void Update()
    {
        TikTok();
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void Reset()
    {
        timeLeft = baseTime;
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
