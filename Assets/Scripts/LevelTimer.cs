using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

/// <summary>
/// Simple timer that ends the level once it reaches
/// zero and loads the given scene.
/// </summary>
public class LevelTimer : Timer
{
    public LevelLoader.SceneEnum nextScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TikTok();

        if (timeLeft <= 0)
        {
            EndLevel();
        }
    }

    /// <summary>
    /// Loads the next scene based on whether
    /// the player destroyed all buildings in time.
    /// </summary>
    void EndLevel()
    {
        LevelLoader.Load(nextScene);
    }
}
