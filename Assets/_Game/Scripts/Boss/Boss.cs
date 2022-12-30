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

    public override void OnInit()
    {
        base.OnInit();
        DeActivateAttack();
    }
    public override void Attack()
    {
        
        ChangeAnim(Constant.ANIM_TRIGGER_ATTACK);
        Invoke(nameof(ActivateAttack), Constant.TIMER_DEDAY_TO_AOE_BOSS);
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
