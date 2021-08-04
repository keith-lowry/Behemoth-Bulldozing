using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

/// <summary>
/// Simple timer that ends the level once time runs out
/// and loads the given scene.
/// </summary>
public class LevelTimer : BasicTimer
{
    public LevelLoader.SceneEnum nextScene;

    // Update is called once per frame
    void Update()
    {
        TikTok();

        if (timeLeft == 0)
        {
            EndLevel();
        }
    }

    /// <summary>
    /// Loads the next scene based on whether
    /// the player destroyed all buildings in time.
    /// </summary>
    private void EndLevel()
    {
        LevelLoader.Load(nextScene);
    }
}
