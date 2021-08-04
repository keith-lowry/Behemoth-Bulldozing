using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for handling the behavior of
/// Quit Buttons.
/// </summary>
public class QuitButton : MonoBehaviour, IButtonBehavior
{
    // Quit Game on click
    public void OnClick()
    {
        Application.Quit();
    }
}
