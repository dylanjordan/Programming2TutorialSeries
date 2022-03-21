using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _damageValue;

    public float _damageMod = 1.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(_damageValue * _damageMod);
            
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(_damageValue * _damageMod);
        }

        Destroy(this.gameObject);

        if (Utility._debug)
        {
            Debug.Log(gameObject.name + " hit " + collision.gameObject.name + " for " + (_damageValue * _damageMod) + " damage");
        }
    }
}
