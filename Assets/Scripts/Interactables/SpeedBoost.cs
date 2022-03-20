using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Interactable
{
    public float _SpeedValue;

    public override void Interact()
    {
        base.Interact();

        Utility.GetPlayerObject().GetComponent<PlayerController>().SpeedBoost(_SpeedValue);

        Destroy(this.gameObject);
    }

}
