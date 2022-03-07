using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D _rb;

    public float _maxHealth = 10.0f;

    float _currentHealth = 10.0f;

    public int _killPointVal = 100;

    Vector3 _dirToPlayer;

    bool _calculatedThisFrame = false;

    public float _enemySpeed = 5.0f;

    public float _contactDamage = 0.5f;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetHealth();
    }

    private void Update()
    {
        UpdateEnemy();
    }

    private void LateUpdate()
    {
        _calculatedThisFrame = false;
    }
    public virtual void UpdateEnemy()
    {
        KillIfDead();
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }

    public void HealDamage(float heal)
    {
        _currentHealth += heal;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void KillIfDead()
    {
        if (CheckIfDead())
        {
            Destroy(gameObject);

            Utility.AddScoreToManager(_killPointVal);

            EnemyManager._totalEnemies--;
        }
    }

    public bool CheckIfDead()
    {
        if (_currentHealth <= 0.0f)
        {
            return true;
        }
        return false;
    }

    public void SeekPlayer(Vector3 dirToPlayer)
    {
        _rb.velocity = dirToPlayer.normalized * _enemySpeed;
       // Debug.Log("Enemy is seeking player");
    }

    public void FleePlayer(Vector3 dirToPlayer)
    {
        _rb.velocity = dirToPlayer.normalized * -_enemySpeed;
    }

    public Vector3 GetDirToPlayer()
    {
        Vector3 dirToPlayer;

        if (!_calculatedThisFrame)
        {
            dirToPlayer = Utility.GetPlayerObject().transform.position - transform.position;

            _dirToPlayer = dirToPlayer;

            _calculatedThisFrame = true;
        }
        else
        {
            dirToPlayer = _dirToPlayer;
        }

        return dirToPlayer;
    }

    public float GetDistToPlayer()
    {
        return GetDirToPlayer().magnitude;
    }

    public void RotateTowardsDir(Vector3 dir)
    {
        float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;

        gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(_contactDamage);
        }
    }
}
