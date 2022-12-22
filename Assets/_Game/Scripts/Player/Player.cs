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
    [SerializeField] GameObject vfxAttack;
    #endregion
    #region Variables: Dash
    [SerializeField] private GameObject DashObj;
    [SerializeField] private GameObject DashVFX;
    public bool isDashing;
    
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
        DeActiveAttack();
        timer =0;
        comboHitStep=-1;
        isAttacking = false;
        isDashing = false;
        isHitting = false;
        DashObj.SetActive(false);
        DashVFX.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.A) /*|| Input.GetMouseButtonDown(0)*/)
        {
            OnAttackAction(); 
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            isDashing = true;
            ChangeAnim("Dash");
            Invoke(nameof(StopDash), 0.7f);
            OnDash();
            JoystickInput.Instance.moveSpeed =speed+5f;
            
        }

        else if(JoystickInput.Instance.isControl && !isAttacking  && !isDashing)
        { 
            ChangeAnim("Run");
            JoystickInput.Instance.moveSpeed =speed;
        }
        else if(!JoystickInput.Instance.isControl && !isAttacking && !isDashing && !isHitting)
        {
            ChangeAnim("Idle");
        }
        // if(!jump && attckBtn.pressed)
        // {
        //     jump = true;
        //     // rb.velocity += Vector3.up*5;
        //     ActiveAttack();
        //     ChangeAnim("Attack");
        // }

        // if(jump && !attckBtn.pressed)
        // {
        //     jump = false;
        //     Debug.Log("after jump");
        // }
    }
    private void OnDash()
    {
        JoystickInput.Instance.moveSpeed =speed+5;
        DashObj.SetActive(true);
        DashVFX.SetActive(true);
    }
    private void StopDash()
    {
        DashObj.SetActive(false);
        DashVFX.SetActive(false);
        JoystickInput.Instance.Stop();
        ChangeAnim("Idle");
        isDashing = false;
        
        
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
            ActiveAttack();
            comboAttackResetCouroutine = StartCoroutine(ResettingAttackCombo());
        }
       
    }

    private  IEnumerator ResettingAttackCombo()
    {
        
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(anim.GetAnimatorTransitionInfo(0).duration);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() =>
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f
           );
        comboHitStep = -1;
        DeActiveAttack();
        anim.SetInteger("hitStep", comboHitStep);
        isAttacking = false;
        
    }

    private void ActiveAttack()
    {
        vfxAttack.SetActive(true);
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        vfxAttack.SetActive(false);
        attackArea.SetActive(false);
    }
}
