using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Enemy
{
    [SerializeField] private Bullet BulletPrefab;
    [SerializeField] private Transform throwPoint;

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void Attack()
    {
        ChangeAnim("Attack");
        Invoke(nameof(DelayAttack), 5f);
        Invoke(nameof(ReloadBullet), 15f);
        Instantiate(BulletPrefab, throwPoint.position, throwPoint.rotation);
    }

    public void ReloadBullet()
    {
        ChangeAnim("Reload");
    }
    public void DelayAttack()
    {
        ChangeAnim("Delay");
    }

}
