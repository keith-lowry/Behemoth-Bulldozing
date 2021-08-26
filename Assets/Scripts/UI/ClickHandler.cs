using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that allows for raycasting to activate
/// Buttons.
/// </summary>
public class ClickHandler : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y); // Get mouse position on screen

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 0f, LayerMask.GetMask("Button"));

            if (hit)
            {
                IButtonBehavior b = hit.collider.gameObject.GetComponent<IButtonBehavior>();
                b.OnClick(); //activate button
            }
        }
    }
}
