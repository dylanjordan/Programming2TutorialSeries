using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : Interactable
{
    public float _upgradeValue = 0.25f;

    public override void Interact()
    {
        PlayerController player = Utility.GetPlayerObject().GetComponent<PlayerController>();

        if (player._weaponInHand != null)
        {
            player._weaponInHand._damageModifier += _upgradeValue;

            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Player is holding no weapon");
        }
    }
}
