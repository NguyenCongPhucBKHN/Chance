using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float randomTime;
    float timer;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;
        randomTime =  Random.Range(enemy.timberIdle[0], enemy.timberIdle[1]);
    }

    public void OnExecute(Enemy enemy)
    {
         timer += Time.deltaTime;

        if (timer > randomTime)
        {
            enemy.ChangeState(new PatrolState()); //  Timer lon hon thoi gian dung, chuyen sang trang thai di chuyen
        }

    }

    public void OnExit(Enemy enemy)
    {
        
    }

    
}
