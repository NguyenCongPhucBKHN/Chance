using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginStage : MonoBehaviour
{
    [SerializeField] GameObject portIn;
   void OnTriggerEnter(Collider other)
   {
    Character character = Cache.GetCharacter(other);
    if(character is Player)
    {
        Debug.Log("Colluder");
        LevelManager.Instance.OnDespawn();
        LevelManager.Instance.DespawnPrevStage();
        Invoke(nameof(ClosePort), 2f);
        LevelManager.Instance.SpawnEnemyWhileInit();
        gameObject.SetActive(false);
    }
   }
   void ClosePort()
   {
    portIn.SetActive(true);
   }
}
