using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy, IHitDash
{
    [SerializeField] private GameObject attackArea;
    [SerializeField] private GameObject attackvfx;
    [SerializeField] private GameObject weaponvfx;

    public override void Awake()
    {
        base.Awake();
        enemyType = EnemyType.Melee;
        timberIdle = Constant.TIMER_BOCK_ATT_MELEE;
        timberPartrol = Constant.TIMER_BOCK_PARTROL_MELEE;
        timerBockAttack = Constant.TIMER_BOCK_ATT_MELEE;
    }
    public override void OnInit()
    {
        base.OnInit();
        DeActiveAttack();
    }
    public  override void Attack()
    {
        ChangeAnim(Constant.ANIM_TRIGGER_ATTACK);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 1f);
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
        attackvfx.SetActive(true);
        weaponvfx.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
        attackvfx.SetActive(false);
        weaponvfx.SetActive(false);
    }
    // public void OnHitDash()
    // {
    //     if(!IsDead)
    //     {
    //         TakeDame(hp);
    //     }

        
    // }
}
