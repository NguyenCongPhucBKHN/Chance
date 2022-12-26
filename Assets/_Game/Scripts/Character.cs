using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit, IHitAttack
{
    [Header("Attributes")]
    public float maxHp =10;
    [SerializeField]
    private float damage;
    // public Transform tf;
    [Header("Movement")]
    public float speed =5f;
    
    public bool IsDead => hp <= 0;
    public bool IsDie;
    [Header("Animator")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected CombatText combatTextPrefab;

    private string currentAnimName;
    protected bool isHitting;
    public float Damage => damage;
    public float hp; //TODO: protected


    
    
    // public virtual void Awake()
    // {
    //     tf= transform;
    // }
    protected void ChangeAnim(string animName)
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

   
    public virtual void OnInit()
    {
        hp =100;
    }

    public virtual void OnDespawn()
    {
        // Destroy(gameObject);
    }
    protected virtual void OnDeath()
    {
        ChangeAnim("Die");
        Invoke(nameof(OnDespawn), 3f);
    }

    public virtual void TakeDame(float damage)
    {
        
        if (!IsDead)
        {
            hp -= damage;

            if (IsDead)
            {
                hp=0;
                OnDeath();
            }
            Instantiate(combatTextPrefab, tf.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }

    public void OnHitAttack(float damage)
    {
        if(!IsDead)
        {
            ChangeAnim("Hit");
            TakeDame(damage);
        }
        
        
    }
}
   
