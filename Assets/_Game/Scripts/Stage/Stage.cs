using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class Stage : MonoBehaviour
{
    public EnemyPath[] paths;
    public DataAmountEnemy dataAmountEnemy;
    public EndStage endStage;

    void Awake()
    {
        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMesh();
    }
    

}
