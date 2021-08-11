using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.PlayerLoop;
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
    public float baseAttack = 10f;

    //most number of times player can attack in a second
    public float maxAttackRate = 3f;

    //least number of times player can attack in a second
    public float minAttackRate = 1f; 

    public KeyCode kickKey = KeyCode.F;
    public KeyCode leftPunchKey = KeyCode.Mouse0;
    public KeyCode rightPunchKey = KeyCode.Mouse1;

    private PlayerController pc;
    private ComboList combos;
    private SpriteRenderer sr;
    private float attack; //attack damage scaled to player's current size
    private float attackRateModifier; //intervals of attack rate decrease
    private float attackRate; //attack rate scaled to player's current size
    private float nextAttack; //time of next attack

    void Start()
    {
        pc = GetComponent<PlayerController>();
        combos = GetComponent<ComboList>();
        sr = GetComponent<SpriteRenderer>();

        nextAttack = 0f;

        //calculate "steps" btw max and min attack rate
        attackRateModifier = (maxAttackRate - minAttackRate) / 
                             (pc.maxScale - pc.minScale);      
        Scale();
    }

    // Update is called once per frame
    void Update()
    {
        // attack control
        if (Input.GetKeyDown(kickKey)) //kick only once per press
        {
            Kick();
        }
        else if (Input.GetKey(leftPunchKey)) //punches repeat
        {
            LeftPunch();
        }
        else if (Input.GetKey(rightPunchKey))
        {
            RightPunch();
        }
    }

    /// <summary>
    /// Scales the player's attack rate, damage,
    /// and the cooldown between their combos
    /// to their current scale.
    /// </summary>
    public void Scale()
    {
        float attackRateDecrease = (pc.GetScale() - pc.minScale) * 
                                   attackRateModifier;

        attackRate = maxAttackRate - attackRateDecrease;

        float attackIncrease = (pc.GetScale() - pc.minScale) * baseAttack;
        attack = baseAttack + attackIncrease;

        combos.Scale();
    }

    /// <summary>
    /// Player kicks in the direction of the
    /// cursor.
    /// </summary>
    private void Kick()
    {
        if (CanAttack(kickKey))
        {
            //animation control

            //TODO: call Dash()

            //make attack
            Attack();
        }
    }

    /// <summary>
    /// Player punches in the direction of the
    /// cursor with their left hand.
    /// </summary>
    private void LeftPunch()
    {
        if (CanAttack(leftPunchKey))
        {
            //animation control

            //TODO: call Dash()

            //make attack
            Attack();
        }
    }

    /// <summary>
    /// Player punches in the direction of the
    /// cursor with their right hand.
    /// </summary>
    private void RightPunch()
    {
        if (CanAttack(rightPunchKey))
        {
            //animation control

            //TODO: call Dash()

            //make attack
            Attack();
        }
    }

    /// <summary>
    /// Makes an attack in front of the player with
    /// a raycast. If cast hits a building, it makes
    /// it take damage equal to the player's attack.
    /// </summary>
    private void Attack()
    {
        nextAttack = Time.time +  (1f / attackRate); //get time of next attack

        Vector2 origin = new Vector2(transform.position.x, 
            transform.position.y); 
        Vector2 direction = pc.GetCursorDirection(); 

        // TODO: tweak raycast size for different player scales?
        RaycastHit2D hit = Physics2D.
            Raycast(origin, direction, sr.size.x * pc.GetScale(), 
                LayerMask.GetMask("Buildings")); //make cast

        if (hit)
        {
            BuildingBehavior bh = hit.transform.gameObject.
                GetComponent<BuildingBehavior>();
            bh.TakeDamage(attack, pc); //make building take damage


            //TODO: implement player knockback
        }
    }

    /// <summary>
    /// Determines whether an attack can be
    /// made.
    ///
    /// An attack can be made if it's associated
    /// key is part of a combo, or if enough
    /// time has elapsed between attacks.
    /// </summary>
    /// <param name="attackKey">
    /// The KeyCode associated with
    /// the attack to be made.
    /// </param>
    /// <returns>
    /// True if an attack can be made, false
    /// otherwise.
    /// </returns>
    private bool CanAttack(KeyCode attackKey)
    {
        return (combos.Add(attackKey) || Time.time >= nextAttack);
    }
}
