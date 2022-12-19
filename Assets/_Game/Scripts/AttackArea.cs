using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class AttackArea : MonoBehaviour
{
    [SerializeField]
    Character ownAttack;
    
    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player!= null)
        {
            Debug.Log("character dame:"+ player.hp);
            player.takeDame(ownAttack.Damage);
        }

        if(collision.CompareTag("Enemy"))
        {
            Character character = collision.GetComponentInParent<Character>();
            Debug.Log("character dame:"+ character.hp);
            character.takeDame(ownAttack.Damage);
        }
    }
}
