using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealth : MonoBehaviour
{
    [Tooltip("Damage should do negative change, healing does positive change")]
    public float amountToChange;
    public bool onlyHitPlayer = false;

    private bool m_HasHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (m_HasHit || other.gameObject.layer == LayerMask.NameToLayer("TransparentFX"))
            return;

        Health otherHealth = other.GetComponentInParent<Health>();

        if (onlyHitPlayer && other.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        if (otherHealth == null)
        {
            Destroy(gameObject);
            return;
        }

        otherHealth.ChangeHealth(amountToChange);
        m_HasHit = true;

        foreach (LingeringGO go in GetComponentsInChildren<LingeringGO>(true))
        {
            go.Activate();
        }

        if (GetComponent<Health>() != null)
            GetComponent<Health>().SelfDestruct();
        else
            Destroy(gameObject);
    }
}
