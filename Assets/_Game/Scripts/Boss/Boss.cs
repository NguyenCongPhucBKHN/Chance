using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] GameObject attackArea;
    [SerializeField] GameObject attackVFX;
    public override void Awake()
    {
        base.Awake();
        enemyType = EnemyType.Boss;
    }

    // public void OnHitDash()
    // {
    //     if(!IsDead)
    //     {
    //         TakeDame(50);
    //     }
    // }
    public override void Attack()
    {
        ActivateAttack();
        ChangeAnim(Constant.ANIM_TRIGGER_ATTACK);
        Invoke(nameof(DeActivateAttack), Constant.TIMER_RUN_AOE);
    }

    public void ActivateAttack()
    {
        attackArea.SetActive(true);
        attackVFX.SetActive(true);
    }

    public void DeActivateAttack()
    {
        attackArea.SetActive(false);
        attackVFX.SetActive(false);
    }

    
}
