using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : GameUnit
{
    public void Despawn()
    {
        SimplePool.Despawn(this);
    }
    
}
