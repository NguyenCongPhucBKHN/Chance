using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        IHitDash hit = other.GetComponentInParent<IHitDash>();
        if(hit != null)
        {
            Debug.Log("hit");
            hit.OnHitDash();
        }
    }
}
