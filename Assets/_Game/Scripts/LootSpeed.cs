using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpeed : Loot
{
    public override void TakeLoot()
    {
        player.IncreaseSpeed(5);
    }

   
}
