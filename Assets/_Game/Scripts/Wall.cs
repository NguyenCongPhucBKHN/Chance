using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall :MonoBehaviour,  IHitAttack, IHitDash
{
    public void OnHitAttack( Transform tf, float damage = 0 )
    {
        Debug.Log("Hit attack wall");
    }

    public void OnHitDash()
    {
        Debug.Log("Hit dash ");
    }
}
