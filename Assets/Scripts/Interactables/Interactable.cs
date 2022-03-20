using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public AudioClip _InteractSound;
    public virtual void Interact()
    {
        AudioSource.PlayClipAtPoint(_InteractSound, transform.position);
    }
}
