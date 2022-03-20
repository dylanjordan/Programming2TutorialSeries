using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public GameObject _bulletPrefab;

    public Transform _firingPoint;

    public int _numBullets = 1;

    public float _spreadAngle = 0.0f;

    public float _bulletSpeed = 15.0f;

    public override void Attack()
    {
        if (_canAttack)
        {
            Fire();

            _canAttack = false;
        }
    }

    public void Fire()
    {
        PlaySound();

        for (int i = 0; i < _numBullets; i++)
        {
            SpawnBullet();
        }
    }

    public void SpawnBullet()
    {
        GameObject spawnedBullet = Instantiate(_bulletPrefab, _firingPoint.position, _firingPoint.rotation);

        float angle = Random.Range(0.0f, (2 * (_spreadAngle)));

        angle -= _spreadAngle;

        spawnedBullet.transform.rotation = Quaternion.AngleAxis(_firingPoint.rotation.eulerAngles.z + angle, Vector3.forward);

        Vector3 direction3D = spawnedBullet.transform.rotation * Vector3.up;
        Vector2 direction2D = new Vector2(direction3D.x, direction3D.y).normalized;

        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(direction2D * _bulletSpeed);

        spawnedBullet.GetComponent<Bullet>()._damageMod = _damageModifier;
    }

    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(_attackSound, transform.position);
    }
}
