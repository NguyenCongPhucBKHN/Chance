using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStage : MonoBehaviour
{
    [SerializeField] Stage stage;
    [SerializeField] GameObject portOut;
    [SerializeField] GameObject bridge;
    Player player;
    Transform playerTF;

void Start()
{
    player = LevelManager.Instance.player;
}
   void Update()
    {
        Vector3 arrow = transform.position - player.tf.position;
        arrow.y =0;
        player.arrowTF.rotation = Quaternion.LookRotation(arrow, Vector3.up);
    }

    void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>(); //TODO Cache
        if(character is Player)
        {
            gameObject.SetActive(false);
            player.arrowTF.gameObject.SetActive(false);
        }
    }

    public void NextStage()
    {
        portOut.SetActive(false);
        bridge.SetActive(true);
        LevelManager.Instance.currentLevel.prevStage = stage;
        LevelManager.Instance.currentLevel.SpawnNewStage();
    }
} 
