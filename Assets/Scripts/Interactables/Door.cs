using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool _moveX = true;
    bool isOpen;

    public float _moveRange = 5.0f;
    public float _timeToClose = 10.0f;

    public override void Interact()
    {
        base.Interact();

        if (!isOpen)
        {
            StartCoroutine(OpenDoorForTime());
        }
    }

    private IEnumerator OpenDoorForTime()
    {
        OpenDoor();

        yield return new WaitForSeconds(_timeToClose);

        CloseDoor();
    }

    private void OpenDoor()
    {
        transform.position += new Vector3(_moveX ? _moveRange : 0.0f, _moveX ? 0.0f : _moveRange, 0.0f);

        isOpen = true;
    }

    private void CloseDoor()
    {
        transform.position -= new Vector3(_moveX ? _moveRange : 0.0f, _moveX ? 0.0f : _moveRange, 0.0f);

        isOpen = false;
    }
}
