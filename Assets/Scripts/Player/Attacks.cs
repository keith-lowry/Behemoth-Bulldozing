using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
// ReSharper disable All

/// <summary>
/// Class that handles animations and
/// mechanics associated with player
/// attacks.
/// </summary>
public class Attacks : MonoBehaviour
{
    public PlayerController pc;
    public int baseDamage;
    public float maxAttackDelay; //largest possible delay between player's attacks
    public float minAttackDelay; //smallest possible delay between player's attacks
    
    private float attackDelayModifier; //intervals of attack delay increase
    private float attackDelay; //scaled delay between attacks
    private SpriteRenderer sr;
    private float nextAttack; //time of next attack

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        nextAttack = 0f;
        attackDelayModifier = (maxAttackDelay - minAttackDelay) / (pc.maxScale - pc.minScale); //calculate "steps" btw max and min delay
        ScaleAttackDelay();
    }

    /// <summary>
    /// Punch in direction of player's cursor with
    /// raycast.
    ///
    /// If raycast hits a building, makes it take damage.
    /// </summary>
    /// <para name="cursorDirection">
    /// Current direction of cursor with respect
    /// to player.
    /// </para>
    public void Punch(Vector2 cursorDirection)
    {
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + attackDelay;

            Vector2 origin = new Vector2(transform.position.x, transform.position.y); //raycast origin
            Vector2 direction = cursorDirection; //raycast direction

            // TODO: tweak raycast size for different player scales?
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, sr.size.x * pc.GetScale(), LayerMask.GetMask("Buildings")); //make cast

            if (hit) //hit building
            {
                BuildingBehavior bh = hit.transform.gameObject.GetComponent<BuildingBehavior>();

                bh.TakeDamage(GetDamage(), pc); //make building take damage
            }
        }

    }

    /// <summary>
    /// Scales the player's attack delay to their
    /// scale.
    /// </summary>
    public void ScaleAttackDelay()
    {
        float increase = (pc.GetScale() - pc.minScale) * attackDelayModifier;
        attackDelay = minAttackDelay + + increase;
    }

    /// <summary>
    /// Calculate the damage the player deals to
    /// a building with an attack.
    /// </summary>
    /// <returns> 
    /// Damage dealt to building.
    /// </returns>
    private int GetDamage()
    {
        return (int) (baseDamage * pc.GetScale()); //damage scales with size
    }
}
