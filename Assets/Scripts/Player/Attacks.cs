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

    // Update is called once per frame
    void Update()
    {
        // attack control
        if (Input.GetKey(KeyCode.Mouse0))
        {
            PunchLeft(); //left punch
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            PunchRight(); //right punch
        }

        if (Input.GetKey(KeyCode.F))
        {
            Kick(); //kick
        }
    }

    /// <summary>
    /// Player punches in the direction of the
    /// cursor with their right hand.
    /// </summary>
    public void PunchRight()
    { 
        //make attack if it's part of a combo or delay has elapsed
        if (combos.Add(KeyCode.Mouse1) || Time.time >= nextAttack)
        {
            //animation control

            //TODO: call Dash()

            Attack();
        }
    }

    /// <summary>
    /// Player punches in the direction of the
    /// cursor with their left hand.
    /// </summary>
    public void PunchLeft()
    {
        //make attack if it's part of a combo or delay has elapsed
        if (combos.Add(KeyCode.Mouse0) || Time.time >= nextAttack)
        {
            //animation control

            //TODO: call Dash()

            Attack();
        }
    }

    /// <summary>
    /// Player kicks in the direction of the
    /// cursor.
    /// </summary>
    public void Kick()
    {
        //make attack if it's part of a combo or delay has elapsed
        if (combos.Add(KeyCode.F) || Time.time >= nextAttack)
        {
            //animation control

            //TODO: call Dash()

            Attack();
        }
    }

    /// <summary>
    /// Scales the player's attack delay to their
    /// scale.
    /// </summary>
    public void ScaleAttackDelay()
    {
        float increase = (pc.GetScale() - pc.minScale) * attackDelayModifier;
        attackDelay = minAttackDelay + increase;
    }

    /// <summary>
    /// Makes an attack in front of the player with
    /// a raycast. If cast hits a building, makes
    /// it take damage.
    /// </summary>
    private void Attack()
    {
        nextAttack = Time.time + attackDelay;

        Vector2 origin = new Vector2(transform.position.x, transform.position.y); //raycast origin
        Vector2 direction = pc.GetCursorDirection(); //raycast direction

        // TODO: tweak raycast size for different player scales?
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, sr.size.x * pc.GetScale(), LayerMask.GetMask("Buildings")); //make cast

        if (hit)
        {
            BuildingBehavior bh = hit.transform.gameObject.GetComponent<BuildingBehavior>();
            bh.TakeDamage(GetDamage(), pc); //make building take damage


            //TODO: implement player knockback
        }
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
