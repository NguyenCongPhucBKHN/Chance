using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Rigidbody rb;
    protected JoystickAttackBtn attckBtn;
    public Transform TF;
    public Transform modelTF;
    protected bool jump;

    private void Awake() {
        attckBtn = FindObjectOfType<JoystickAttackBtn>();  
    }

    
    void Start()
    {
     attckBtn = FindObjectOfType<JoystickAttackBtn>();  
     TF = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(JoystickInput.Instance.isControl)
        {
            Debug.Log("Run");
            ChangeAnim("Run");
            modelTF.rotation =  Quaternion.LookRotation(JoystickInput.Instance.move, Vector3.up);
            
        }
        else if(!JoystickInput.Instance.isControl)
        {
            ChangeAnim("Idle");
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            ChangeAnim("Attack");

        }



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
