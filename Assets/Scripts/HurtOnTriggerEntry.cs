using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnTriggerEntry : MonoBehaviour
{
    public float amountToChange = -1;

    private void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.GetComponentInParent<Health>();

        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || otherHealth == null)
            return;

        otherHealth.ChangeHealth(amountToChange);
    }
}
