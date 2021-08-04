using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script with list of KeyCodes under
/// the hood to handle Combos behavior.
///
/// Allows three attacks to be made in quick
/// succession as long as they are unique. A
/// combo can be completed at any time once it
/// is started.
/// </summary>
public class ComboList : MonoBehaviour
{
    private List<KeyCode> keys;

    // Start is called before the first frame update
    void Start()
    {
        keys = new List<KeyCode>(3); //initialize list with capacity of 3
                                     //for 3 types of attacks
    }

    /// <summary>
    /// Adds the given attack KeyCode to the list.
    /// Clears the list if the given KeyCode fills
    /// the list or is already in the list.
    /// 
    /// Returns true if the corresponding attack should receive no
    /// attack delay, false if it should. Attacks that start a 
    /// combo and attacks already in the list receive a delay.
    /// </summary>
    /// <param name="keyPressed">
    /// The attack key pressed and to be added to the
    /// keys list.
    /// </param>
    /// <returns>
    /// True if the attack should receive no
    /// attack delay, false otherwise.
    /// </returns>
    public bool Add(KeyCode keyPressed)
    {
        if (keys.Contains(keyPressed)) //not a unique attack in combo
        {
            keys.Clear(); //clear list, combo lost
            return false;
        }
        else //unique attack in combo
        {
            if (IsEmpty()) //first attack in combo, has delay
            {
                keys.Add(keyPressed);
                return false;
            }

            keys.Add(keyPressed); //add key, combo continued

            if (IsFull()) //completed combo
            {
                keys.Clear(); //clear list
            }

            return true;
        }
    }

    /// <summary>
    /// Checks whether the list of
    /// KeyCodes is full.
    /// </summary>
    /// <returns>
    /// True if the list is full,
    /// false otherwise.
    /// </returns>
    private bool IsFull()
    {
        return (keys.Capacity == keys.Count);
    }

    /// <summary>
    /// Checks whether the list of
    /// KeyCodes is empty.
    /// </summary>
    /// <returns>
    /// True if the list is empty,
    /// false otherwise.
    /// </returns>
    private bool IsEmpty()
    {
        return (keys.Count == 0);
    }
}
