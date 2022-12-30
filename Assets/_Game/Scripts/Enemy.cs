using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
public class Enemy : Character, IHitDash
{
    [SerializeField] private float attackRange;
    [SerializeField] private float fovRadius;
    [SerializeField] protected CapsuleCollider capsuleCollider;
    protected EnemyType enemyType;
    public NavMeshAgent agent;
    Vector3 point;
    Vector3 destination;
    private IState currentState;
    private Character target;
    public Character Target => target;
    public EnemyPath path;
    public List<Transform> listPoint ; //TODO
    public bool isAttack =false;

    public bool IsDestination 
    {
        get 
        {
            point = tf.position;
            point.y = destination.y;
            return Vector3.Distance(destination, point) < attackRange;
        }
    }
    public bool isTargetInFov; 
    public virtual void Awake() 
    {
        tf = transform;
        
    }
    private void Start() {
        GetComponent<SphereCollider>().radius = fovRadius;
        agent.speed = speed;
        
    }

    
    private void Update()
    {
        if(currentState!= null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Character player = Cache.GetCharacter(other);
        // Player player = other.GetComponent<Player>();
        if(player is Player)
        {
            target = player;
            isTargetInFov = true;
        }
    }
     


    public override void OnInit()
    {
        base.OnInit();
        DeactiveTrigger();
        listPoint = path.WayPoints;
        ChangeState( new IdleState());
    }
    public override void OnDespawn()
    {
        target = null;
        ChangeState(null);
        SimplePool.Despawn(this);
    }
    protected override void  OnDeath()
    {
        target= null;
        ChangeState(null);
        base.OnDeath();
        LevelManager.Instance.UpdateCounter(enemyType);
        LevelManager.Instance.SpawnWhileEnemyDead(enemyType);
    }
     public void OnHitDash()
    {
        capsuleCollider.isTrigger = true;
        if(!IsDead)
        {   
            TakeDame(15);
            Invoke(nameof(DeactiveTrigger), 1f);
        }

    }
    public void DeactiveTrigger()
    {
        capsuleCollider.isTrigger = false;   
    }

     public void SetDestination(Vector3 position)
    {
        destination = position;
        destination.y = 0;
        agent.SetDestination(destination);
    }


     public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void StopMoving()
    {
        ChangeAnim(Constant.ANIM_TRIGGER_IDLE);
        agent.enabled= false;
    }
    public virtual void Moving()
    {
        agent.enabled= true;
        ChangeAnim(Constant.ANIM_TRIGGER_RUN);
        if(Target!=null)
        {
            SetDestination(Target.tf.position);
        }
        else
        {
            ChangeAnim(Constant.ANIM_TRIGGER_WALK);
            int index = Random.Range(0, listPoint.Count);
            SetDestination(listPoint[index].position);
        }
        
    }

    public virtual void Attack()
    {

    }

     public bool IsTargetInRange()
    {
        
        Vector3 point = target.transform.position;
        point.y = tf.position.y;
        if (target != null && Vector3.Distance(point, tf.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeDirection(Transform playerTF)
    {
        tf.rotation = Quaternion.LookRotation(playerTF.position -tf.position, Vector3.up);     
    }
    public Vector3 GetTargetPoint()
    {
        int index = Random.Range(0, listPoint.Count);
        return listPoint[index].position;
    }
    
    public void SubAmount()
    {
       switch (enemyType) {
        case EnemyType.Melee:
            LevelManager.Instance.meleeDead ++;
            LevelManager.Instance.meleeCouter--;
            break;
        case EnemyType.Range:
            LevelManager.Instance.rangeDead ++;
            LevelManager.Instance.rangeCouter--;
            break;
        case EnemyType.Boss:
            LevelManager.Instance.bossDead ++;
            LevelManager.Instance.bossCouter--;
            break;
        default :
            
            break;
       }
    }
    public void Reset()
    {
        target = null;
        ChangeAnim(Constant.ANIM_TRIGGER_IDLE);
        ChangeState(null);
    }
}
