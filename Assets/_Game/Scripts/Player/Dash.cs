using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {

        
        IHitDash hit = Cache.GetIHitDashInParent(other);
        if(hit != null)
        {
            hit.OnHitDash();
        }
    }
}
