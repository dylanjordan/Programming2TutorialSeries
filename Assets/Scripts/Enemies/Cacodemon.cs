using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cacodemon : Enemy
{
    public Gun _fireBallCannon;

    public float _fleeDist = 5.0f;

    public override void UpdateEnemy()
    {
        base.UpdateEnemy();

        RotateTowardsDir(GetDirToPlayer());

        if (GetDistToPlayer() < _fleeDist)
        {
            FleePlayer(GetDirToPlayer());
        }
        else
        {
            SeekPlayer(GetDirToPlayer());
        }
        FireAtPlayer();
    }

    public void FireAtPlayer()
    {
        if (_fireBallCannon != null)
        {
            _fireBallCannon.Attack();
        }
    }
}
