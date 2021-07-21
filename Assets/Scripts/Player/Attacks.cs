using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
// ReSharper disable All

/// <summary>
/// Script that handles the player's
/// attacks, including animations and
/// making attacks against buildings
/// and other players.
/// </summary>
public class Attacks : MonoBehaviour
{
    public float baseDamage = 10f;
    public float maxAttackRate = 3f; //most number of times player can attack in a second
    public float minAttackRate = 1f; //least number of times player can attack in a second

    public PlayerController pc;
    public ComboList combos;
    private SpriteRenderer sr;
    private float attackRateModifier; //intervals of attack rate decrease
    private float attackRate; //scaled number of times player can attack in a second
    private float nextAttack; //time of next attack

    void Start()
    {
        pc = GetComponent<PlayerController>();
        combos = GetComponent<ComboList>();
        sr = GetComponent<SpriteRenderer>();

        nextAttack = 0f;
        attackRateModifier = (maxAttackRate - minAttackRate) / (pc.maxScale - pc.minScale); //calculate "steps" btw max and min delay
        ScaleAttackRate();
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
    public void ScaleAttackRate()
    {
        float decrease = (pc.GetScale() - pc.minScale) * attackRateModifier;
        attackRate = maxAttackRate - decrease;
    }

    /// <summary>
    /// Makes an attack in front of the player with
    /// a raycast. If cast hits a building, it makes
    /// it take damage.
    /// </summary>
    private void Attack()
    {
        nextAttack = Time.time +  (1f / attackRate); //calculate time of next attack

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
