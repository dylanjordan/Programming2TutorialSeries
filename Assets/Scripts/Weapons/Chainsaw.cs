using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw : Weapon
{
    public float _damageValue = 0.5f;

    public AudioSource _idleSource;
    private void Awake()
    {
        _idleSource.clip = _attackSound;
        _idleSource.Play();
    }

    public override void Attack()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_canAttack)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Enemy enemy = other.attachedRigidbody.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(_damageValue * _damageModifier);

                _canAttack = false;
            }
        }
    }
}
