using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy, IHitDash
{
    public void OnHitDash()
    {
        takeDame(100);
    }
}
