using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform TF;
    public Player player;
    public Vector3 offset;
    public float lerpRate;
    // public bool gameOver;
    

    // Start is called before the first frame update
    // void Start()
    // {
    //     gameOver = false;
        
    // }
    void Awake()
    {
        player = FindObjectOfType<Player>();
        TF= gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player!= null)
        {
            Follow();
        }
        
        // if( !GameManagerr.Instance.IsState(EGameState.Finish)  )
        // {
        //     Follow();
        // }
    }

    void Follow()
    {
        Vector3 pos = TF.position;
        Vector3 targetPos = player.TF.position + offset;
        pos = Vector3.Lerp(pos, targetPos, lerpRate*Time.fixedDeltaTime);
        TF.position = pos;
    }
    
    public void FollowEndGame(Vector3 position)
    {
        Vector3 pos = TF.position;
        Vector3 targetPos = position + offset;
        TF.position = targetPos;
    }
}
