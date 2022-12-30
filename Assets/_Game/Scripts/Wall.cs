using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall :MonoBehaviour,  IHitAttack, IHitBullet
{
    [SerializeField] GameObject hitPrefab;
    [SerializeField] GameObject hitBulletPrefab;
    GameObject hit;
    GameObject hitBullet;
    public void OnHitAttack( Transform hitTF, float damage = 0 )
    {   
        hit = Instantiate(hitPrefab, hitTF.position, hitTF.rotation);
        
        Invoke(nameof(DestroyHitAttackVfx), 1f);
     
        
    }

    public void OnHitBullet(Transform hitTF, float damage = 0)
    {
        hitBullet = Instantiate(hitBulletPrefab, hitTF.position, hitTF.rotation);
        Invoke(nameof(DestroyHitBullet), 1f);

    }
    public void DestroyHitAttackVfx()
    {
        Destroy(hit);
    }
    public void DestroyHitBullet()
    {
        Destroy(hitBullet);
    }
}
