using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Interactable
{
    public Transform _healPoint;

    public GameObject _healParticle;

    public float _healValue = 50.0f;
    public override void Interact()
    {
        base.Interact();

        Utility.GetPlayerObject().GetComponent<PlayerController>().HealDamage(_healValue);

        GameObject spawnedHeal = Instantiate(_healParticle, _healPoint.position, _healPoint.rotation, _healPoint);
        Destroy(spawnedHeal, 1.0f);

        Destroy(this.gameObject);
    }
}
