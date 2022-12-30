using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    public void OnEnter(Enemy enemy)
    {
        if (enemy.Target != null && !enemy.Target.IsDead)
        {
            enemy.ChangeDirection(enemy.Target.tf);
            enemy.StopMoving();
            enemy.Attack();
        }

        timer = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        
        timer += Time.deltaTime;
        float timerWait = Random.Range(enemy.timerBockAttack[0], enemy.timerBockAttack[1]);
        if (timer >= timerWait)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
