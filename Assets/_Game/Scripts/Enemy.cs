using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fovRadius;
    public NavMeshAgent agent;
    Vector3 point;
    Vector3 destination;
    private IState currentState;
    private Character target;
    public Character Target => target;
    public List<Transform> listPoint;
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
    public override void Awake() 
    {
        base.Awake();
        
    }
    private void Start() {
        GetComponent<SphereCollider>().radius = fovRadius;
        OnInit();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    /// 
    /// 
    private void Update()
    {
        if(currentState!= null)
        {
            currentState.OnExecute(this);
        }
        else
        {
            ChangeState( new PatrolState());
        }
    }

    private void OnTriggerEnter(Collider other) {
        Player player = other.GetComponent<Player>();
        if(player!= null)
        {
            target = player;
            isTargetInFov = true;
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeState( new IdleState());
    }

     public void SetDestination(Vector3 position)
    {
        //destination = position;

        destination = position;
        destination.y = 0;
        Moving();
        agent.SetDestination(position);
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
        ChangeAnim("Idle");
        agent.SetDestination(tf.position);
    }
    public void Moving()
    {
        ChangeAnim("Run");
        if(Target!=null)
        {
            agent.SetDestination(Target.tf.position);
        }
        else
        {
            int index = Random.Range(0, listPoint.Count);
            agent.SetDestination(listPoint[index].position);

        }
        //TODO: Set agent move
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
}
