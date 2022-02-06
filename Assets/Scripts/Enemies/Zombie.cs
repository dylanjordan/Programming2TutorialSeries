using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public override void UpdateEnemy()
    {
        base.UpdateEnemy();

        RotateTowardsDir(GetDirToPlayer());

        SeekPlayer(GetDirToPlayer());
    }
}
