using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// using UnityEditor.AI;

public class Stage : MonoBehaviour
{
    public EnemyPath[] paths;
    public DataAmountEnemy dataAmountEnemy;
    public EndStage endStage;
    public NavMeshSurface navMeshSurface;

    

    void Awake()
    {
        navMeshSurface.BuildNavMesh();
        // NavMeshBuilder.ClearAllNavMeshes();
        // NavMeshBuilder.BuildNavMesh();
    }
    

}
