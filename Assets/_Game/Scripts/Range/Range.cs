using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Enemy
{
    [SerializeField] private Bullet BulletPrefab;
    [SerializeField] private Transform throwPoint;

    public override void Awake()
    {
        base.Awake();
        enemyType = EnemyType.Range;
        enemyType = EnemyType.Melee;
        timberIdle = Constant.TIMER_BOCK_ATT_RANGE;
        timberPartrol = Constant.TIMER_BOCK_PARTROL_RANGE;
        timerBockAttack = Constant.TIMER_BOCK_ATT_RANGE;
    }
    public override void OnInit()
    {
        base.OnInit();
    }

    public override void Attack()
    {
        ChangeAnim(Constant.ANIM_TRIGGER_ATTACK);
        SimplePool.Spawn<Bullet>(PoolType.Bullet, throwPoint.position, throwPoint.rotation).OnInit();
        // Invoke(nameof(DelayAttack), 5f); //BUG sau khi die van tan cong
        // Invoke(nameof(ReloadBullet), 15f);

    }

    public void ReloadBullet()
    {
        ChangeAnim(Constant.ANIM_TRIGGER_RELOAD);
    }
    public void DelayAttack()
    {
        ChangeAnim("Delay");
    }
    

}
