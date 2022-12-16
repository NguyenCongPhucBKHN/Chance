using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHit
{
    [SerializeField] private Animator anim;

    private string currentAnimName;
    private float hp;
    public bool IsDead =>hp<=0;
    public virtual void takeDame()
    {
        
    }

    protected void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void OnHit(float damage)
    {
        Debug.Log("Hit");
        if (!IsDead)
        {
            hp -= damage;

            if (IsDead)
            {
                hp = 0;
                OnDeath();
            }

            // healthBar.SetNewHp(hp);
        }
    }

    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        // Invoke(nameof(OnDespawn), 2f);
    }
}
