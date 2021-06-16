using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuitButton : MonoBehaviour, UIButtonBehavior
{
    // Quit Game on click
    public void OnClick()
    {
        Application.Quit();
    }
}
