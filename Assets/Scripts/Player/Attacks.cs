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
    public ComboList combos;
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
    /// Player punches in the direction of the
    /// cursor with their right hand.
    /// </summary>
    /// <param name="cursorDirection">
    /// The direction of the cursor relative
    /// to the player transform.
    /// </param>
    public void PunchRight(Vector2 cursorDirection)
    { 
        //make attack if delay has elapsed or this is a unique attack
        if (combos.Add(KeyCode.Mouse1) || Time.time >= nextAttack)
        {
            RaycastHit2D hit = Attack(cursorDirection);

            if (hit)
            {
                BuildingBehavior bh = hit.transform.gameObject.GetComponent<BuildingBehavior>();
                bh.TakeDamage(GetDamage(), pc); //make building take damage
                

                //TODO: implement player knockback
            }
        }
    }

    /// <summary>
    /// Player punches in the direction of the
    /// cursor with their left hand.
    /// </summary>
    /// <param name="cursorDirection">
    /// The direction of the cursor relative
    /// to the player transform.
    /// </param>
    public void PunchLeft(Vector2 cursorDirection)
    {
        //make attack if delay has elapsed or this is a unique attack
        if (combos.Add(KeyCode.Mouse0) || Time.time >= nextAttack)
        {
            RaycastHit2D hit = Attack(cursorDirection);

            if (hit)
            {
                BuildingBehavior bh = hit.transform.gameObject.GetComponent<BuildingBehavior>();
                bh.TakeDamage(GetDamage(), pc); //make building take damage
                
                //TODO: implement player knockback
            }
        }
    }

    /// <summary>
    /// Player kicks in the direction of the
    /// cursor.
    /// </summary>
    /// <param name="cursorDirection">
    /// The direction of the cursor relative
    /// to the player transform.
    /// </param>
    public void Kick(Vector2 cursorDirection)
    {
        //make attack if delay has elapsed or this is a unique attack
        if (combos.Add(KeyCode.F) || Time.time >= nextAttack)
        {
            RaycastHit2D hit = Attack(cursorDirection);

            if (hit)
            {
                BuildingBehavior bh = hit.transform.gameObject.GetComponent<BuildingBehavior>();
                bh.TakeDamage(GetDamage(), pc); //make building take damage

                //TODO: implement player knockback
            }
        }
    }

    /// <summary>
    /// Calls PlayerController's Dash()
    /// method.
    /// </summary>
    public void Dash()
    {
        pc.Dash();
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
    /// Makes an attack in front of the player with
    /// a raycast and returns the raycast hit.
    /// </summary>
    /// <param name="cursorDirection"></param>
    /// <returns></returns>
    private RaycastHit2D Attack(Vector2 cursorDirection)
    {
        nextAttack = Time.time + attackDelay;

        Vector2 origin = new Vector2(transform.position.x, transform.position.y); //raycast origin
        Vector2 direction = cursorDirection; //raycast direction

        // TODO: tweak raycast size for different player scales?
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, sr.size.x * pc.GetScale(), LayerMask.GetMask("Buildings")); //make cast

        return hit;
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
