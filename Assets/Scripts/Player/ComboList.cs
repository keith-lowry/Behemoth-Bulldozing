using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboList : MonoBehaviour
{
    public Attacks at;

    private List<KeyCode> keys;

    // Start is called before the first frame update
    void Start()
    {
        keys = new List<KeyCode>(3); //initialize list with capacity of 3
                                     //for 3 types of attacks
    }

    /// <summary>
    /// Adds the given KeyCode to the list.
    /// 
    /// Clears the list once it is filled or
    /// if the key to add is not unique. A
    /// not unique key will be added after the
    /// list is cleared.
    /// </summary>
    /// <param name="keyPressed">
    /// They attack key pressed and to be added to the
    /// keys list.
    /// </param>
    /// <returns>
    /// True if the key was unique, false otherwise.
    /// </returns>
    public bool Add(KeyCode keyPressed)
    {
        if (keys.Contains(keyPressed))
        {
            //Debug.Log("RESET: ComboList already has that");
            keys.Clear(); //clear list, combo lost
            keys.Add(keyPressed); //add anyways hehexD
            return false;
        }
        else
        {
            //Debug.Log("Key added");

            keys.Add(keyPressed); //add key, combo continued

            if (IsFull())
            {
                //Debug.Log("Combo Completed"); //combo completed
                keys.Clear(); //clear list
                at.Dash(); //combo bonus movement
            }

            return true;
        }
    }
    /// <summary>
    /// Checks whether the list of
    /// KeyCodes is full.
    /// </summary>
    /// <returns></returns>
    private bool IsFull()
    {
        return (keys.Capacity == keys.Count);
    }
}
