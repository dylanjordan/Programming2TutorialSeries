using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Interactable
{

    public float _healValue = 50.0f;
    public override void Interact()
    {
        base.Interact();

        Utility.GetPlayerObject().GetComponent<PlayerController>().HealDamage(_healValue);

        Destroy(this.gameObject);
    }
}
