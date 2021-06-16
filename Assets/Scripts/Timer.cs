using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple timer.
/// </summary>
public class Timer : MonoBehaviour
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
    /// Decreases timeLeft.
    /// </summary>
    public void TikTok()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;;

            if (timeLeft <= 0)
            {
                timeLeft = 0;
            }
        }
        
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void Reset()
    {
        timeLeft = baseTime;
    }

}
