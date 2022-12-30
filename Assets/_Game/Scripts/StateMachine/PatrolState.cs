using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float randomTime;
    float timer;
    
    
    public void OnEnter(Enemy enemy)
    {
        
        timer = 0;
        randomTime = Random.Range(enemy.timberPartrol[0], enemy.timberPartrol[1]);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.Target != null) // Neu co muc tieu 
        {
            //doi huong enemy toi huong cua player
            enemy.ChangeDirection(enemy.Target.tf); // Chuyen huong theo muc tieu

            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState()); // Neu player trong khoang than cong thi chuyen sang trang thai tan cong
            }
            else
            {
                enemy.Moving(); 
            }

        }
        else 
        {
            if (timer < randomTime)    
            {
                enemy.Moving(); 
            }
            else 
            {
                enemy.ChangeState(new IdleState()); //
            }
        }

    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
