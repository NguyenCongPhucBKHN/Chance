using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHitAttack
{
    [Header("Attributes")]
    public float maxHp =10;
    [SerializeField]
    private float damage;
    public Transform tf;
    [Header("Movement")]
    public float speed =5f;
    
    public bool IsDead => hp <= 0;
    [Header("Animator")]
    [SerializeField] protected Animator anim;

    private string currentAnimName;
    
    public float Damage => damage;
    public float hp; //TODO: protected
    
    public virtual void Awake()
    {
        tf= transform;
    }
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
        Destroy(gameObject);
    }
    protected virtual void OnDeath()
    {
        ChangeAnim("Die");
        Invoke(nameof(OnDespawn), 3f);
    }

    public virtual void takeDame(float damage)
    {
        
        if (!IsDead)
        {
            hp -= damage;

            if (IsDead)
            {
                OnDeath();
            }
        }
    }

    public void OnHitAttack(float damage)
    {
        ChangeAnim("Hit");
        takeDame(damage);
        
    }
}
   
