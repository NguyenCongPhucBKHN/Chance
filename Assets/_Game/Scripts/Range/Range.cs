using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Enemy
{
    public override void OnInit()
    {
        base.OnInit();

    }

    public override void Attack()
    {
        ChangeAnim("Attack");
    }

    // public override void Moving()
    // {
    //     if(Target!= null)
    //     {
    //         ChangeAnim("Run");
    //         agent.SetDestination(Target.tf.position);
    //     }
    //     else
    //     {
    //         ChangeAnim("Walk");
    //         int index = Random.Range(0, listPoint.Count);
    //         agent.SetDestination(listPoint[index].position);
    //     }
    // }
}
