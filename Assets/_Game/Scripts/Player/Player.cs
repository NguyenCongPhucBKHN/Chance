using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    protected JoystickAttackBtn attckBtn;
    public Transform TF;
    protected bool jump;

    // Start is called before the first frame update
    void Start()
    {
     attckBtn = FindObjectOfType<JoystickAttackBtn>();  
     TF = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!jump && attckBtn.pressed)
        {
            jump = true;
            rb.velocity += Vector3.up*5;
        }

        if(jump && !attckBtn.pressed)
        {
            jump = false;
            Debug.Log("after jump");
        }
    }
}
