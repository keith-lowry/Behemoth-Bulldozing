using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for handling the behavior of
/// UI quit buttons.
/// </summary>
public class UIQuitButton : MonoBehaviour, UIButtonBehavior
{
    // Quit Game on click
    public void OnClick()
    {
        Application.Quit();
    }
}
