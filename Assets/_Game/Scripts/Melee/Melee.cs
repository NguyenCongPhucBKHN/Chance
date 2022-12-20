using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy
{
    [SerializeField] private GameObject attackArea;
    public override void OnInit()
    {
        base.OnInit();
        DeActiveAttack();
    }
    public  override void Attack()
    {
        ChangeAnim("Attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 1f);
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
}
