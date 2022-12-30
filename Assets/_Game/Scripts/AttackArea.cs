using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class AttackArea : MonoBehaviour
{
    [SerializeField] Transform tf;
    [SerializeField] Character ownAttack;
    [SerializeField] GameObject hitSlashPrefab;
    // Hit hit;
    
    private void OnTriggerEnter(Collider collision)
    {
        IHitAttack hit = Cache.GetIHitAttackInParent(collision);
        if(hit!=null)
        {
            hit.OnHitAttack(tf, ownAttack.Damage);
        }
    }

  
}
