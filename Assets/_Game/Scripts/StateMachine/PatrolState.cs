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
        randomTime = Random.Range(3f, 6f); 
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
                enemy.Moving(); // Neu khong co muc tieu thi cu di chuyen  ( di chuyen ve huong muc tieu)
            }

        }
        else // Neu khong co muc tieu
        {
            if (timer < randomTime)    // Timer nho hon thoi gian di chuyen
            {
                enemy.Moving(); // Di chuyen
            }
            else // Neu Timer lon hon thoi gian di chuyen thi chuyen sang trang thai dung
            {
                enemy.ChangeState(new IdleState()); //
            }
        }

    }

    public void OnExit(Enemy enemy)
    {
        // throw new System.NotImplementedException();
    }
}
