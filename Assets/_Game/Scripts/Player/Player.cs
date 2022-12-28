using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Character
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private GameObject attackArea;
    [SerializeField] public Transform arrowTF;
    protected JoystickAttackBtn attckBtn;
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
    [SerializeField] private GameObject dashObj;
    [SerializeField] private GameObject dashVfx;
    private bool dashRequest;
    private bool enableDash = true;
    #endregion

    #region  Variables: RotationAttack
    [SerializeField] private GameObject rotationObj;
    private bool rotationRequest;
    private bool enableRotate= true;
    #endregion
    
    #region Variables: AOE
    [SerializeField] private GameObject aoeObj;
    private bool aoeRequest;
    private bool enableAoe = true;
    #endregion  

    #region Variables: Inputs
    private PlayerControllAction inputActions;
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction dashAction;
    private InputAction rotationAction;
    private InputAction aoeAction;
    #endregion
    private Vector2 moveInput ;
    private Vector3 move;
    private bool isRunning;
    public  void Awake() 
    {
        attckBtn = FindObjectOfType<JoystickAttackBtn>();  
        inputActions = new PlayerControllAction();
        tf= transform;
    }

    private void OnEnable() {
        moveAction = inputActions.Player.Move;
        moveAction.Enable();

        attackAction= inputActions.Player.Attack;
        attackAction.performed += OnAttackAction;
        attackAction.Enable();

        dashAction = inputActions.Player.Dash;
        dashAction.performed += OnDashAction;
        dashAction.Enable();

        rotationAction = inputActions.Player.RotationAttack;
        rotationAction.performed += OnRotationAction;
        rotationAction.Enable();

        aoeAction = inputActions.Player.AOE;
        aoeAction.performed += OnAOEAction;
        aoeAction.Enable();

    }

    

    private void OnDisable() {
        moveAction.Disable();
    }
    void Start()
    {
     attckBtn = FindObjectOfType<JoystickAttackBtn>();  
     OnInit();
     
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if(dashRequest)
        {
            if(enableDash)
            {
                Dash();
            }
            else
            {
                dashRequest = false;
            }
        }
        else if(aoeRequest)
        {
            if(enableAoe)
            {
                AOE();
            }
            else
            {
                aoeRequest = false;
            }
        }
        else if(rotationRequest)
        {
            if(enableRotate)
            {
                Rotation();
            }
            else
            {
                rotationRequest = false;
            }
        }
        else if(isAttacking)
        {
           
        }
        else if(IsDead)
        {

        }
        else if(moveInput.SqrMagnitude() >0.001f )
        {
            RotationModel();
            // isRunning = true;
            Move(speed);
            ChangeAnim(Constant.ANIM_TRIGGER_RUN);
        }
       
        
        
        else /*if(moveInput.SqrMagnitude() <0.001f)*/
        {
            ChangeAnim(Constant.ANIM_TRIGGER_IDLE);
            Debug.Log("Stop player");
            Stop();
        }
          
    }

     public override void OnInit()
    {
        base.OnInit();
        DeActiveAttack();
        timer =0;
        comboHitStep=-1;
        isRunning = false;
        isAttacking = false;
        dashRequest = false;
        isHitting = false;
        aoeRequest = false;
        dashObj.SetActive(false);
        dashVfx.SetActive(false);
        aoeObj.SetActive(false);
        rotationObj.SetActive(false);
        arrowTF.gameObject.SetActive(false);
        
    }

    public void RotationModel()
    {
        
        move = new Vector3(moveInput.x, 0f, moveInput.y);
        modelTF.rotation =  Quaternion.LookRotation(move, Vector3.up);
    }

    public void Move(float speed)
    {   move.Normalize();
        rb.velocity = new Vector3(move.x *speed, rb.velocity.y, move.z*speed);

    }

    public  IEnumerator WaitingForCurrentAnimation(
            Animator animator,
            System.Action callback,
            float earlyExit = 0f,
            string waitForAnimName = null,
            float extraWait = 0f,
            bool stopAfterAnim = false)
        {
            if (stopAfterAnim)
            {
                yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(
                    animator.GetAnimatorTransitionInfo(0).duration);
                yield return new WaitForEndOfFrame();
                    
                yield return new WaitUntil(() =>
                    animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f);
                    RotationModel();
            }
            else if (waitForAnimName == null)
            {
                yield return new WaitForEndOfFrame();

                yield return new WaitForSeconds(
                    animator.GetAnimatorTransitionInfo(0).duration);
                yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(
                    animator.GetCurrentAnimatorStateInfo(0).length - earlyExit);
            }
            else
            {
                yield return new WaitUntil(() =>
                    animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == waitForAnimName);
                yield return new WaitForSeconds(
                    animator.GetCurrentAnimatorStateInfo(0).length - earlyExit);
            }
            if (extraWait > 0)
                yield return new WaitForSeconds(extraWait);
            callback();
        }
    private void OnDashAction(InputAction.CallbackContext obj)
    {
        dashRequest= true;
        
    }
    private void Dash()
    {
        ChangeAnim(Constant.ANIM_TRIGGER_DASH);
        Invoke(nameof(StopDash), Constant.TIMER_RUN_DASH);
        Dash(speed+5);
        dashObj.SetActive(true);
        dashVfx.SetActive(true);
    }

    private void OnRotationAction(InputAction.CallbackContext obj)
    {
        rotationRequest= true;
    }
    private void Rotation()
    {
        ActiveAttack();
        ChangeAnim(Constant.ANIM_TRIGGER_ROTATION);
        Invoke(nameof(StopRotation), Constant.TIMER_RUN_ROTATION);
    }

    private void OnAOEAction(InputAction.CallbackContext obj)
    {
        aoeRequest = true;
        

    }
    private void AOE() 
    {
        ChangeAnim(Constant.ANIM_TRIGGER_AOE);
        Invoke(nameof(StopAOE), Constant.TIMER_RUN_AOE);
    }
    
    

    
    public void Dash(float speed)
    {
         rb.velocity = modelTF.forward * speed;
    }
    private void StopDash()
    {
        dashRequest = false;
        enableDash =false;
        dashObj.SetActive(false);
        dashVfx.SetActive(false);
        StartCoroutine(EnableDash());
       
        
    }

    private IEnumerator EnableDash()
    {
        yield return new WaitForSeconds(Constant.TIMER_BLOCK_DASH);
        enableDash = true;
    }

    private void StopRotation()
    {
        rotationObj.SetActive(false);
        DeActiveAttack();
        rotationRequest = false;
        enableRotate = false;
        StartCoroutine(EnableRotation());

        
    }
    private IEnumerator EnableRotation()
    {
        yield return new WaitForSeconds(Constant.TIMER_BLOCK_ROTATION);
        enableRotate = true;
    }
    private void StopAOE()
    {
        aoeRequest = false;
        enableAoe = false;
        aoeObj.SetActive(false);
        StartCoroutine(EnableAOE());
    }
    private IEnumerator EnableAOE()
    {
        yield return new WaitForSeconds(Constant.TIMER_BLOCK_AOE);
        enableAoe = true;
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }

    private void OnAttackAction(InputAction.CallbackContext obj)
    {
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
            anim.SetInteger(Constant.ANIM_LABEL_HIT_STEP, comboHitStep);
            ChangeAnim(Constant.ANIM_TRIGGER_ATTACK);
            ActiveAttack();
            comboAttackResetCouroutine = StartCoroutine(
                WaitingForCurrentAnimation(anim,()=>
                
                {
                    ResetAttackCombo();
                    moveInput = moveAction.ReadValue<Vector2>();
                    if(move.sqrMagnitude>0.01f )
                    {
                        ChangeAnim(Constant.ANIM_TRIGGER_RUN);
                        RotationModel();
                        Move(speed);
                    }
                    else
                    {
                        Stop();
                        ChangeAnim(Constant.ANIM_TRIGGER_IDLE);
                    }
                },
                stopAfterAnim: true,
                earlyExit: 0.2f
                ));
        }
       
    }

    public void ResetAttackCombo()
        {
            comboHitStep = -1;
            isAttacking = false;
            DeActiveAttack();
            anim.SetInteger(Constant.ANIM_LABEL_HIT_STEP, comboHitStep);
            
        }
    public override void OnDespawn()
    {
        // gameObject.SetActive(false);
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        LevelManager.Instance.OnFinishGame();
        UIManager.Instance.CloseUI<Gameplay>();
        UIManager.Instance.OpenUI<Fail>();
        GameManager.Instance.ChangeState(GameState.Pause);
        
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
        anim.SetInteger(Constant.ANIM_LABEL_HIT_STEP, comboHitStep);
        isAttacking = false;
        isRunning = false;
        
    }

     public override void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
        else if(currentAnimName ==Constant.ANIM_TRIGGER_ATTACK)
        {
            anim.SetTrigger(currentAnimName);
        }
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
    public void ActivateArrow(Transform endStage)
    {
        Vector3 arrow = endStage.position - tf.position;
        arrow.y =0;
        arrowTF.rotation = Quaternion.LookRotation(arrow, Vector3.up);
    }
    public void DeactivaeArrow()
    {
        arrowTF.gameObject.SetActive(false);
    }
}
