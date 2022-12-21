using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class AttackArea : MonoBehaviour
{
    [SerializeField] Transform tf;
    [SerializeField] Character ownAttack;
    [SerializeField] GameObject hitSlashPrefab;
    
    private void OnTriggerEnter(Collider collision)
    {
        Character character = collision.GetComponentInParent<Character>();
        if(character!=null)
        {
            Instantiate(hitSlashPrefab, tf.position, tf.rotation);
            character.OnHitAttack(ownAttack.Damage);
            
        }
    }
}
