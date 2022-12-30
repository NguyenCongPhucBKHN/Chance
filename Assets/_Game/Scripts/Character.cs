using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatTextSystem;
using HealthBarSystem;
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
    [SerializeField] protected HealthBar healthBar;

    protected string currentAnimName;
    protected bool isHitting;
    public float Damage => damage;
    public float hp; //TODO: protected


    
    
    // public virtual void Awake()
    // {
    //     tf= transform;
    // }
    public virtual void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

   
    public virtual void OnInit()
    {
        hp =100;
        healthBar.Init(tf, 100, 0, 100);
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
                OnDeath();
                hp=0;
                
            }
            else if(this is Player)
            {
                CombatTextManager.Instance.CreateText(tf.position, $"-{damage}", Color.red, true);
            }
            else
            {
                CombatTextManager.Instance.CreateText(tf.position, $"-{damage}", Color.green, true);
            }
            healthBar.DecreaseHealth(damage);
            healthBar.HandleHealthBar();
            // SimplePool.Spawn<CombatText>(PoolType.CombatText, tf.position + Vector3.up, Quaternion.identity).OnInit(damage);
            // Instantiate(combatTextPrefab, tf.position + Vector3.up, Quaternion.identity).OnInit(damage);
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
   
