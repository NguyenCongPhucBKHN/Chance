using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class AttackArea : MonoBehaviour
{
    [SerializeField] Transform tf;
    [SerializeField] Character ownAttack;
    [SerializeField] GameObject hitSlashPrefab;
    Hit hit;
    
    private void OnTriggerEnter(Collider collision)
    {
        IHitAttack character = Cache.GetIHitAttackInParent(collision);
        if(character!=null)
        {
            hit = SimplePool.Spawn<Hit>(PoolType.HitSlash, tf.position, tf.rotation);
            character.OnHitAttack(ownAttack.Damage);
            Invoke(nameof(DestroyHitVfx), 3f);
        }
    }

    public void DestroyHitVfx()
    {
        hit.Despawn();
    }
}
