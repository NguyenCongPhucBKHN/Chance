using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : GameUnit
{
    [SerializeField] GameObject lootVFX;
    protected Player player;
    public void OnInit()
    {
        lootVFX.SetActive(true);
    }
    private void OnTriggerEnter(Collider other) {
        Character character = Cache.GetCharacter(other);
        if(character is Player)
        {
            
            player = (Player) character;
            TakeLoot(); 
            Despawn();
            // lootVFX.SetActive(false);
        }
    }

    public  void Despawn()
    {
        SimplePool.Despawn(this);
    }
    public virtual void TakeLoot()
    {

    }
}
