using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Enemy, IHitDash
{
    [SerializeField] private Bullet BulletPrefab;
    [SerializeField] private Transform throwPoint;

    public override void Awake()
    {
        base.Awake();
        enemyType = EnemyType.Range;
    }
    public override void OnInit()
    {
        base.OnInit();
    }

    public override void Attack()
    {
        ChangeAnim(Constant.ANIM_TRIGGER_ATTACK);
        Invoke(nameof(DelayAttack), 5f);
        Invoke(nameof(ReloadBullet), 15f);
        SimplePool.Spawn<Bullet>(PoolType.Bullet, throwPoint.position, throwPoint.rotation).OnInit();
        // Instantiate(BulletPrefab, throwPoint.position, throwPoint.rotation);
    }

    public void ReloadBullet()
    {
        ChangeAnim("Reload");
    }
    public void DelayAttack()
    {
        ChangeAnim("Delay");
    }
    public void OnHitDash()
    {   
        if(!IsDead)
        {
            TakeDame(hp);
        }
    }

}
