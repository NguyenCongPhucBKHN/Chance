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
    public bool isDashing;
    #endregion

     #region Variables: Inputs
     private PlayerControllAction inputActions;
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction dashAction;
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

    }
    private void OnDisable() {
        moveAction.Disable();
    }
    void Start()
    {
     attckBtn = FindObjectOfType<JoystickAttackBtn>();  
     OnInit();
     
    }

    public override void OnInit()
    {
        base.OnInit();
        DeActiveAttack();
        timer =0;
        comboHitStep=-1;
        isRunning = false;
        isAttacking = false;
        isDashing = false;
        isHitting = false;
        dashObj.SetActive(false);
        dashVfx.SetActive(false);
        arrowTF.gameObject.SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if(isDashing)
        {
        }
        
        else if(moveInput.SqrMagnitude() >0.001f &&!isAttacking && !isDashing)
        {
            RotationModel();
            // isRunning = true;
            Move(speed);
            ChangeAnim("Run");
        }
       
        else if(isAttacking)
        {
            // RotationModel();
        }
        else if(IsDead)
        {

        }
        else if(moveInput.SqrMagnitude() <0.001f)
        {
            ChangeAnim("Idle");
            Stop();
        }
          
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
    public void MoveToPoint(Transform Point)
    {

        Vector3 point = Point.position;
        point.y = tf.position.y;
        while(Vector3.Distance(tf.position, point)>0.1f)
        {
            moveInput = new Vector2(point.x-tf.position.x, point.y-tf.position.y);
            RotationModel();
            // isRunning = true;
            Move(speed);
            ChangeAnim("Run");
        }
    }
    private void OnDashAction(InputAction.CallbackContext obj)
    {
        isDashing= true;
        ChangeAnim("Dash");
        Invoke(nameof(StopDash), 0.7f);
        Dash(speed+5);
        dashObj.SetActive(true);
        dashVfx.SetActive(true);
    }

    public void Dash(float speed)
    {
         rb.velocity = modelTF.forward * speed;
    }
    private void StopDash()
    {
        isDashing = false;
        dashObj.SetActive(false);
        dashVfx.SetActive(false);
        Stop();
        ChangeAnim("Idle");
        
        
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }
    private void OnAttackAction(InputAction.CallbackContext obj)
    {
        // JoystickInput.Instance.moveSpeed =0;
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
        anim.SetInteger("hitStep", comboHitStep);
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
        else if(currentAnimName =="Attack")
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
