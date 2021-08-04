using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for handling the behavior of
/// Scene Transition Buttons.
/// </summary>
public class TransitionButton : MonoBehaviour, IButtonBehavior
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
