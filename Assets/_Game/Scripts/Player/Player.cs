using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private GameObject attackArea;
    protected JoystickAttackBtn attckBtn;
    public Transform TF;
    public Transform modelTF;
    protected bool jump;
    private float timer;
    private float T = 3;
    private int hitNumber;
    
    #region Variables: Attack
    private float COMBO_MIN_DELAY =0.1f;
    private int COMBO_MAX_STEP=2;
    private int comboHitStep;
    private bool isAttacking;
    private Coroutine comboAttackResetCouroutine;
    #endregion

    public override void Awake() 
    {
        attckBtn = FindObjectOfType<JoystickAttackBtn>();  
    }

    void Start()
    {
     attckBtn = FindObjectOfType<JoystickAttackBtn>();  
     TF = transform;
     OnInit();
     
    }

    public override void OnInit()
    {
        base.OnInit();
        timer =0;
        isAttacking = false;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.A) /*|| Input.GetMouseButtonDown(0)*/)
        {
            OnAttackAction(); 
        }

        else if(JoystickInput.Instance.isControl && !isAttacking)
        { 
            ChangeAnim("Run");
            JoystickInput.Instance.moveSpeed =speed;
        }
        else if(!JoystickInput.Instance.isControl && !isAttacking)
        {
            ChangeAnim("Idle");
        }

        


        if(!jump && attckBtn.pressed)
        {
            jump = true;
            // rb.velocity += Vector3.up*5;
            ActiveAttack();
            ChangeAnim("Attack");
        }

        if(jump && !attckBtn.pressed)
        {
            jump = false;
            Debug.Log("after jump");
        }
    }

    private void OnAttackAction()
    {
        JoystickInput.Instance.moveSpeed =0;
        isAttacking = true;
        if(comboHitStep == COMBO_MAX_STEP)
            return;
        float t = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if(comboHitStep ==-1 || ( t>=0.1f && t<=0.8f))
        {
            if(comboAttackResetCouroutine != null)
            {
                StopCoroutine(comboAttackResetCouroutine);
            }
            comboHitStep++;
            anim.SetInteger("hitStep", comboHitStep);
            ChangeAnim("Attack");
            comboAttackResetCouroutine = StartCoroutine(ResettingAttackCombo());
        }
       
    }

    private  IEnumerator ResettingAttackCombo()
    {
        
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(anim.GetAnimatorTransitionInfo(0).duration);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() =>
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
        comboHitStep = -1;
        anim.SetInteger("hitStep", comboHitStep);
        isAttacking = false;
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
}
