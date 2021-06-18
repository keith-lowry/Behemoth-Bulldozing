using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All

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
    /// <param name="mc">
    /// PlayerController of the player inflicting
    /// damage.
    /// </param>
    public void TakeDamage(int damage, PlayerController mc)
    {
        mc.timer.Reset(); //reset shrink timer

        if (mc.GetScale() >= scale)
        {
            health -= damage;
        }

        Debug.Log(health);

        if (health <= 0)
        {
            DestroyBuilding(mc);
        }
    }

    /// <summary>
    /// Destroys this building and
    /// grows the player.
    /// </summary>
    private void DestroyBuilding(PlayerController mc)
    {
        mc.Grow(scale); //grow player
        
        this.gameObject.SetActive(false);
    }
}
