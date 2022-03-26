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
    bool _onCooldown = false;
    
    //Abstract attack function
    public abstract void Attack();

    private IEnumerator AttackWithCoolDown()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(_attackRate);
        _canAttack = true;
        _onCooldown = false;
    }

    public void Update()
    {
        //Update whether we can attack
        if (!_canAttack && !_onCooldown)
        {
            StartCoroutine(AttackWithCoolDown());
        }
    }
}
