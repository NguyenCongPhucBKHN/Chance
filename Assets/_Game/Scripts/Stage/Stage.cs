using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // public EnemyDatas enemyDatas;
    public EnemyPath[] paths;
    public DataAmountEnemy dataAmountEnemy;
    public int meleeCouter;
    public int rangeCouter;
    public int bossCouter;
 
    // public void OnInit()
    // {
    //     NewEnemy(EnemyType.Melee);
    // }

    // public void NewEnemy(EnemyType enemyType)
    // {
    //     Enemy enemyPrefab = enemyDatas.GetPrefabEnemy(enemyType);
    //     int counter=0;
    //     int maxEnemy;
    //    switch (enemyType) 
    //    {
    //     case EnemyType.Melee:
    //         counter = meleeCouter;
    //         maxEnemy = dataAmountEnemy.numberMelee;
    //         break;
    //     case EnemyType.Range:
    //         counter = rangeCouter;
    //         maxEnemy = dataAmountEnemy.numberRange;
    //         break;
    //     case EnemyType.Boss:
    //         counter = bossCouter;
    //         maxEnemy = dataAmountEnemy.numberBoss;
    //         break;
    //     default :
    //         counter = 0;
    //         maxEnemy = 0;
    //         break;
    //    }
    //     for(int i = counter; i< maxEnemy; i++)
    //     {
    //         int pathId = Random.Range(0, paths.Length);
    //         int pointId = Random.Range(0, paths[pathId].WayPoints.Count);
    //         Enemy enemy = Instantiate(enemyPrefab, paths[pathId].WayPoints[pointId].position, Quaternion.identity);
    //         enemy.path = paths[pathId];
    //         counter ++;
    //         IncreaseEnemy(enemyType);
    //     }
        
    // }
    
    // public  void IncreaseEnemy(EnemyType enemyType)
    // {
    //     switch (enemyType) 
    //    {
    //     case EnemyType.Melee:
    //         meleeCouter ++;
    //         break;
    //     case EnemyType.Range:
    //         rangeCouter++;
    //         break;
    //     case EnemyType.Boss:
    //         bossCouter++;
    //         break;
    //     default :
    //         break;
    //    }
    // }
}
