using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _damageValue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(_damageValue);
            
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(_damageValue);
        }

        Destroy(this.gameObject);
    }
}
