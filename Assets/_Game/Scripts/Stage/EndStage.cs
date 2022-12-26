using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStage : MonoBehaviour
{
    [SerializeField] Stage stage;
    [SerializeField] GameObject portOut;
    [SerializeField] GameObject bridge;

    void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>(); //TODO Cache
        if(character is Player)
        {
            portOut.SetActive(false);
            bridge.SetActive(true);
            LevelManager.Instance.currentLevel.prevStage = stage;
            LevelManager.Instance.currentLevel.SpawnNewStage();
        }
    }
} 
