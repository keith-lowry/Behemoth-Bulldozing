using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that makes the main Camera
/// follow the player.
/// </summary>
public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    void Awake()
    {
        offset = transform.position - player.transform.position; // offset btw player and camera
    }

    // Update is called once per frame
    void Update()
    {
        // Track player
        float xPos = player.transform.position.x - offset.x;
        float yPos = player.transform.position.y - offset.y;

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
