using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LevelManager : Singleton<LevelManager>
{
    public Level[] levels;
    // public  Melee meleePrefab;
    // public Range rangePrefab;
    // public Boss bossPrefab;
    public EnemyDatas enemyDatas;
    public List<Enemy> enemies = new List<Enemy>();
    public Player player;
    public Vector3 savePointPlayer;
    private Level currentLevel;
    private int levelIndex;

    #region  Variable parameter enemy in stage
    public int TotalMelee => currentLevel.currentStage.dataAmountEnemy.totalMelee;
    public int TotalRange =>  currentLevel.currentStage.dataAmountEnemy.totalRange;
    public int TotalBoss =>  currentLevel.currentStage.dataAmountEnemy.totalBoss;
    public int NumberMelee =>  currentLevel.currentStage.dataAmountEnemy.numberMelee;
    public int NumberRange =>  currentLevel.currentStage.dataAmountEnemy.numberRange;
    public int NumberBoss =>  currentLevel.currentStage.dataAmountEnemy.numberBoss;
    public EnemyPath[] Paths => currentLevel.currentStage.paths;
    public int   meleeDead; //TODO: private
    public int   rangeDead;
    public int   bossDead;

    public int meleeCouter;
    public int rangeCouter;
    public int bossCouter;
    #endregion
    
    private void Awake() {
        currentLevel = levels[0]; 
        levelIndex =0; //TODO Set level index
    }

    private void Start() 
    {
        RetryLevel();
        //TODO: Load level
        // triangulation = NavMesh.CalculateTriangulation();
        OnInit();
        // StartCoroutine(IESpawnsEnemies());

    }
    public void RetryLevel()
    {

    }
    public void NextLevel()
    {
        OnDespawn();
        LoadLevel(1);
        OnInit();
    }

    private void OnDespawn()
    {
        for(int i =0; i< enemies.Count; i++)
        {
            SimplePool.Despawn(enemies[i]);
        }
        enemies.Clear();
    }
    public void LoadLevel(int index)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[index]);
    }

    public void OnInit()
    {
        player.tf.position = savePointPlayer;
        player.OnInit();
        currentLevel.OnInit();
        SpawnEnemyWhileInit();
        
        
    }
    public void SpawnEnemyWhileInit()
    {
        SpawnEnemyAmount(EnemyType.Melee, NumberMelee);
        SpawnEnemyAmount(EnemyType.Range, NumberRange);
        StartCoroutine(IESpawnBoss());
    }

    public void UpdateCounter(EnemyType enemyType)
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

    public IEnumerator IESpawnBoss()
    {
        yield return new WaitUntil(()=> meleeDead == TotalMelee && rangeDead == TotalRange);
        Debug.Log("Spawn boss");
        SpawnEnemyAmount(EnemyType.Boss, 1);
    }

    public bool EnableSpawn(EnemyType enemyType)
    {
        if(enemyType == EnemyType.Melee)
        {
            
            return  meleeDead + meleeCouter < TotalMelee;
        }
        else if(enemyType == EnemyType.Range)
        {
           
            return rangeDead + rangeCouter< TotalRange;
        }
        else
        {
           
            return  bossDead+ bossCouter< TotalBoss;
        }
    }
    public void SpawnWhileEnemyDead(EnemyType enemyType)
    {
        if(EnableSpawn(enemyType))
        {
            SpawnEnemyAmount(enemyType, 1);
        }
    }
    public void SpawnEnemyAmount(EnemyType enemyType, int amount)
    {
        Enemy enemyPrefab = enemyDatas.GetPrefabEnemy(enemyType);
        for(int i =0; i< amount; i++)
        {
            int pathId = Random.Range(0, Paths.Length);
            int pointId = Random.Range(0, Paths[pathId].WayPoints.Count);
            Enemy enemy = Instantiate(enemyPrefab, Paths[pathId].WayPoints[pointId].position, Quaternion.identity);// SimplePool.Spawn<Enemy>(PoolType.Enemy, Paths[pathId].WayPoints[pointId].position, Quaternion.identity);
            enemy.path = Paths[pathId];
            IncreaseEnemy(enemyType);
            enemies.Add(enemy);
        }
    }
    public void SpawnEnemyType(EnemyType enemyType)
    {
        Enemy enemyPrefab = enemyDatas.GetPrefabEnemy(enemyType);
        int counter=0;
        int maxEnemy;
       switch (enemyType) 
       {
        case EnemyType.Melee:
            counter = meleeCouter;
            maxEnemy = NumberMelee;
            break;
        case EnemyType.Range:
            counter = rangeCouter;
            maxEnemy = NumberRange;
            break;
        case EnemyType.Boss:
            counter = bossCouter;
            maxEnemy = NumberBoss;
            break;
        default :
            counter = 0;
            maxEnemy = 0;
            break;
       }
        for(int i = counter; i< maxEnemy; i++)
        {
            int pathId = Random.Range(0, Paths.Length);
            int pointId = Random.Range(0, Paths[pathId].WayPoints.Count);
            Enemy enemy = Instantiate(enemyPrefab, Paths[pathId].WayPoints[pointId].position, Quaternion.identity);
            enemy.path = Paths[pathId];
            counter ++;
            IncreaseEnemy(enemyType);
        }
        
    }
    public  void IncreaseEnemy(EnemyType enemyType)
    {
        switch (enemyType) 
       {
        case EnemyType.Melee:
            meleeCouter ++;
            break;
        case EnemyType.Range:
            rangeCouter++;
            break;
        case EnemyType.Boss:
            bossCouter++;
            break;
        default :
            break;
       }
    }

//     private IEnumerator IESpawnBot()
//     {
//         yield return new WaitUntil(()=>(rangeSpawned == TotalRange && meleeSpawned == TotalMelee));
//         // SpawnAEnemy(EnemyType.Boss);

//     }

//     //Cho spawn
//     // private IEnumerator IESpawnsEnemies()
//     // {
       
        
//     //     yield return StartCoroutine(IESpawnsEnemie(EnemyType.Melee, NumberMelee,  meleePrefab , Paths[0]));
//     //     yield return StartCoroutine(IESpawnsEnemie( EnemyType.Range, NumberRange, rangePrefab , Paths[0]));
            
        
//     //     if(meleeSpawned==TotalMelee && rangeSpawned==TotalRange && bossSpawned< NumberBoss)
//     //     {
//     //         yield return StartCoroutine(IESpawnsEnemie( EnemyType.Boss, NumberBoss, bossPrefab , Paths[0]));
//     //         // amountCurrentBoss++;
//     //         // bossSpawned++;
//     //     }
       
        
//     // }

//     //Spawn 1 loai
// //    private IEnumerator  IESpawnsEnemie( EnemyType enemyType, float  amoutAct, Enemy enemyPrefab ,EnemyPath path)
// //     {
// //         while (true)
// //         {
// //             yield return new WaitForSeconds(EnemySpawnInterval); 
// //             // float amout = GetAmout(enemyType);
            
// //             // if(EnableSpawn(enemyType, amoutAct))
// //             {
// //                 //TODO: speed
// //                 Enemy enemy = Instantiate(enemyPrefab, path.WayPoints[0].position, Quaternion.identity);
// //                 enemy.listPoint = path.WayPoints;
// //                 IncrAmout(enemyType);
// //                 enemy.OnInit();
                
// //             }
// //         }
        
        
// //     }

//     public void IncrAmout(EnemyType enemyType)
//     {
       
//        switch (enemyType) {
//         case EnemyType.Melee:
//             meleeCouter++;
//             meleeSpawned++;
//             break;
//         case EnemyType.Range:
//             rangeCouter++;
//             rangeSpawned++;
//             break;
//         case EnemyType.Boss:
//             bossCouter++;
//             bossSpawned++;
//             break;
//         default :
            
//             break;
//        }
//     }

//     public int GetAmout(EnemyType enemyType)
//     {
//         if(enemyType == EnemyType.Melee)
//         {
//             return meleeCouter;
//         }
//         else if(enemyType == EnemyType.Range)
//         {
//             return rangeCouter;
//         }
//         else
//         {
//             return bossCouter;
//         }
//     }

//     public bool EnableSpawn(EnemyType enemyType)
//     {
//         if(enemyType == EnemyType.Melee)
//         {
            
//             return meleeCouter < NumberMelee && meleeSpawned < TotalMelee;
//         }
//         else if(enemyType == EnemyType.Range)
//         {
           
//             return rangeCouter < NumberRange && rangeSpawned < TotalRange;
//         }
//         else
//         {
           
//             return bossCouter < NumberBoss && bossSpawned< TotalBoss;
//         }
//     }

   

//     public void SpawnEnemy(EnemyType enemyType)
//     {
        
//         if(EnableSpawn(enemyType))
//         {
            
//             // SpawnAEnemy(enemyType);
              
//         }
//     }




















//     public void SpawnAEnemy(Enemy enemyPrefab, Vector3 point)
//     {
//         Enemy enemy = Instantiate(enemyPrefab, point, Quaternion.identity);
//     }


//     // public Vector3 ChooseClosedPoint()
//     // {
//     //     float distance = Mathf.Infinity;
//     //     Vector3 point = listSpawnTf[0].position;
//     //     for(int i =0; i< listSpawnTf.Count; i++)
//     //     {
//     //         if(Vector3.Distance(listSpawnTf[i].position, player.tf.position)< distance)
//     //         {
//     //             point = listSpawnTf[i].position;
//     //         }
//     //     }
//     //     return point;
//     // }


//     // public void SpawnNumberEnemy(Enemy enemyPrefab, float number)
//     // {
//     //     Vector3 point = ChooseClosedPoint();
//     //     SpawnAEnemy(enemyPrefab, point);
//     //     for(int i =1; i< number; i++)
//     //     {
//     //         int index = Random.Range(0, listSpawnTf.Count);
//     //         Vector3 pos = listSpawnTf[index].position;
//     //         SpawnAEnemy(enemyPrefab, pos);
//     //     }
//     // }


//     // public void SpawnAEnemy(Enemy enemyPrefab)
//     // {
        
//     //     int VertexIndex = Random.Range(0, triangulation.vertices.Length);
//     //     NavMeshHit Hit;
//     //     if (NavMesh.SamplePosition(triangulation.vertices[VertexIndex], out Hit,  0.1f,  NavMesh.AllAreas))
//     //     {
//     //         if(Hit.mask>0.5)
//     //         {
//     //             Enemy enemy = Instantiate(enemyPrefab);
//     //             enemy.agent.Warp(Hit.position);
//     //             Debug.DrawRay(Hit.position, Vector3.up*100, Color.red, Mathf.Infinity);
//     //             enemy.OnInit();
//     //         }
            
            
//     //     }
//     // }

    

}
