using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// Interface for handling main behavior for
/// UI buttons when clicked.
/// </summary>
public interface UIButtonBehavior
{

    /// <summary>
    /// Performs the Button's main action.
    /// </summary>
    public void OnClick();

}
