using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy, IHitDash
{
    public override void Awake()
    {
        base.Awake();
        enemyType = EnemyType.Boss;
    }

    public override void Attack()
    {
        // base.Attack();
        ChangeAnim("Attack");
    }
    public void OnHitDash()
    {
        if(!IsDead)
        {
            TakeDame(50);
        }
    }
}
