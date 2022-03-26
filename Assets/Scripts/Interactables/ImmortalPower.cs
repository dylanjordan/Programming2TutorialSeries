using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalPower : Interactable
{

    public float _ImmortalityTime = 2.0f;

    public void Start()
    {
        
    }
    public override void Interact()
    {
        base.Interact();

        StartCoroutine(ImmortalityPower());
    }

    private IEnumerator ImmortalityPower()
    {
        Utility.GetPlayerObject().GetComponent<PlayerController>().IsImmortal(true);
        gameObject.transform.position = new Vector3(232, -82, 0);

        Debug.Log("Immortal active");

        yield return new WaitForSeconds(_ImmortalityTime);

        Debug.Log("Not Immortal Anymore");

        Utility.GetPlayerObject().GetComponent<PlayerController>().IsImmortal(false);
        Destroy(gameObject);
    }
}
