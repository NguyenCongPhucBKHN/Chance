using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy, IHitDash
{
    [SerializeField] private GameObject attackArea;

    public override void Awake()
    {
        base.Awake();
        enemyType = EnemyType.Melee;
    }
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
    public void OnHitDash()
    {
        // Debug.Log("Dash collider");
        OnDeath();
        
    }
}
