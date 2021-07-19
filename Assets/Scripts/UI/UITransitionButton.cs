using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for behavior of scene transition
/// UI buttons.
/// </summary>
public class UITransitionButton : MonoBehaviour, UIButtonBehavior
{
    public LevelLoader.SceneEnum nextScene;

    /// <summary>
    /// Loads the next scene on click.
    /// </summary>
    public void OnClick()
    {
        LevelLoader.Load(nextScene);
    }
}
