using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All

/// <summary>
/// Behavior script for Building objects. Buildings
/// can take damage from players and be destroyed.
/// </summary>
public class BuildingBehavior : MonoBehaviour
{
    public int health;
    public int scale;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Makes the bulding take damage from
    /// player if the player's scale is greater
    /// than or equal to that of the building.
    /// 
    /// Calls DestroyBuilding() if health 
    /// reaches zero.
    /// </summary>
    /// <param name="damage">
    /// Damage to be taken by the building.
    /// </param>
    /// <param name="pc">
    /// PlayerController of the player inflicting
    /// damage.
    /// </param>
    public void TakeDamage(int damage, PlayerController pc)
    {
        pc.GetShrinkTimer().Reset(); //reset shrink timer

        if (pc.GetScale() >= scale)
        {
            health -= damage;
        }

        Debug.Log(health);

        if (health <= 0)
        {
            DestroyBuilding(pc);
        }
    }

    /// <summary>
    /// Destroys this building and
    /// grows the player.
    /// </summary>
    private void DestroyBuilding(PlayerController pc)
    {
        if (!pc.AtMaximumScale()) 
        {
            pc.Grow(scale); //grow player
        }

        this.gameObject.SetActive(false);
    }
}
