using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatTextSystem;
using HealthBarSystem;

public class LootHP : Loot
{
    public override void TakeLoot()
    {
        player.IncreaseHP(10);
        CombatTextManager.Instance.CreateText(player.tf.position, $"+{10}", Color.red, true);
        player.IncreaseHP(10);
    }
    
}
