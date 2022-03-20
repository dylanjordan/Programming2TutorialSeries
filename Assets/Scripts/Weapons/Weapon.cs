using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //Attack sound
    public AudioClip _attackSound;

    //Rate of attack for weapon
    public float _attackRate;
    public float _damageModifier = 1.0f;
    //Time since last attack
    protected float _timeSinceAttack = 0.0f;
    //Can the weapon attack
    protected bool _canAttack = true;
    
    //Abstract attack function
    public abstract void Attack();

    public void UpdateCanAttack()
    {
        //if can't attack
        if (!_canAttack)
        {
            //Add time passed since last check
            _timeSinceAttack += Time.deltaTime;

            //Check if time is past attack rate
            if (_timeSinceAttack >= _attackRate)
            {
                //We can attack
                _canAttack = true;
                //Reset time since attack
                _timeSinceAttack = 0.0f;
            }
        }
    }

    public void Update()
    {
        //Update whether we can attack
        UpdateCanAttack();
    }
}
