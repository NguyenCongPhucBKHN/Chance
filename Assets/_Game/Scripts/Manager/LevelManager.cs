using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LevelManager : Singleton<LevelManager>
{
    public Level[] level;
    public  Melee meleePrefab;
    public Range rangePrefab;
    public Boss bossPrefab;
    public Player player;
    public Vector3 savePointPlayer;
    public List<Transform> listSpawnTf => currentLevel.listSpawnEnemyTf;
    public EnemyPath[] Paths => currentLevel.paths;
    public EnemyDatas enemyDatas;
    public float EnemySpawnInterval => currentLevel.enemySpawnInterval; //Khoang thoi gian
    // private NavMeshTriangulation triangulation;
    private Level currentLevel;
    private int levelIndex;

    #region  Variable parameter enemy in level
    public int TotalMelee => currentLevel.totalMelee;
    public int TotalRange => currentLevel.totalRange;
    public int TotalBoss => currentLevel.totalBoss;
    public int NumberMelee => currentLevel.numberMelee;
    public int NumberRange => currentLevel.numberRange;
    public int NumberBoss => currentLevel.numberBoss;

    public int   meleeSpawned; //TODO: private
    public int   rangeSpawned;
    public int   bossSpawned;

    public int amoutCurrentMelee;
    public int amountCurrentRange;
    public int amountCurrentBoss;
    #endregion
    
    private void Awake() {
        currentLevel = level[0]; 
        levelIndex =0; //TODO Set level index
    }

    private void Start() 
    {
          
        //TODO: Load level
        // triangulation = NavMesh.CalculateTriangulation();
        OnInit();
        // StartCoroutine(IESpawnsEnemies());

    }

    public void OnInit()
    {
        player.tf.position = savePointPlayer;
        player.OnInit();
        for(int i =0; i< NumberMelee; i++)
        {
            SpawnEnemy(EnemyType.Melee);

        }
        for(int i =0; i< NumberMelee; i++)
        {
            SpawnEnemy(EnemyType.Range);
        }
        StartCoroutine(IESpawnBot());
        
    }

    private IEnumerator IESpawnBot()
    {
        yield return new WaitUntil(()=>(rangeSpawned == TotalRange && meleeSpawned == TotalMelee));
        SpawnAEnemy(EnemyType.Boss);

    }

    //Cho spawn
    private IEnumerator IESpawnsEnemies()
    {
       
        
        yield return StartCoroutine(IESpawnsEnemie(EnemyType.Melee, NumberMelee,  meleePrefab , Paths[0]));
        yield return StartCoroutine(IESpawnsEnemie( EnemyType.Range, NumberRange, rangePrefab , Paths[0]));
            
        
        if(meleeSpawned==TotalMelee && rangeSpawned==TotalRange && bossSpawned< NumberBoss)
        {
            yield return StartCoroutine(IESpawnsEnemie( EnemyType.Boss, NumberBoss, bossPrefab , Paths[0]));
            // amountCurrentBoss++;
            // bossSpawned++;
        }
       
        
    }

    //Spawn 1 loai
   private IEnumerator  IESpawnsEnemie( EnemyType enemyType, float  amoutAct, Enemy enemyPrefab ,EnemyPath path)
    {
        while (true)
        {
            yield return new WaitForSeconds(EnemySpawnInterval); 
            // float amout = GetAmout(enemyType);
            
            // if(EnableSpawn(enemyType, amoutAct))
            {
                //TODO: speed
                Enemy enemy = Instantiate(enemyPrefab, path.WayPoints[0].position, Quaternion.identity);
                enemy.listPoint = path.WayPoints;
                IncrAmout(enemyType);
                enemy.OnInit();
                
            }
        }
        
        
    }

    public void IncrAmout(EnemyType enemyType)
    {
       
       switch (enemyType) {
        case EnemyType.Melee:
            amoutCurrentMelee++;
            meleeSpawned++;
            break;
        case EnemyType.Range:
            amountCurrentRange++;
            rangeSpawned++;
            break;
        case EnemyType.Boss:
            amountCurrentBoss++;
            bossSpawned++;
            break;
        default :
            
            break;
       }
    }

    public int GetAmout(EnemyType enemyType)
    {
        if(enemyType == EnemyType.Melee)
        {
            return amoutCurrentMelee;
        }
        else if(enemyType == EnemyType.Range)
        {
            return amountCurrentRange;
        }
        else
        {
            return amountCurrentBoss;
        }
    }

    public bool EnableSpawn(EnemyType enemyType)
    {
        if(enemyType == EnemyType.Melee)
        {
            
            return amoutCurrentMelee < NumberMelee && meleeSpawned < TotalMelee;
        }
        else if(enemyType == EnemyType.Range)
        {
           
            return amountCurrentRange < NumberRange && rangeSpawned < TotalRange;
        }
        else
        {
           
            return amountCurrentBoss < NumberBoss && bossSpawned< TotalBoss;
        }
    }

    public void SpawnAEnemy(EnemyType enemyType)
    {
        Enemy enemyPrefab = enemyDatas.GetPrefabEnemy(enemyType);
        int index = Random.Range(0, listSpawnTf.Count);
        Vector3 point = listSpawnTf[index].position;
        EnemyPath path = Paths[Random.Range(0, Paths.Length)];
        Enemy enemy = Instantiate(enemyPrefab, path.WayPoints[0].position, Quaternion.identity);
        enemy.listPoint = path.WayPoints;
        enemy.OnInit();
        IncrAmout(enemyType);
    }

    public void SpawnEnemy(EnemyType enemyType)
    {
        
        if(EnableSpawn(enemyType))
        {
            
            SpawnAEnemy(enemyType);
              
        }
    }




















    public void SpawnAEnemy(Enemy enemyPrefab, Vector3 point)
    {
        Enemy enemy = Instantiate(enemyPrefab, point, Quaternion.identity);
    }


    public Vector3 ChooseClosedPoint()
    {
        float distance = Mathf.Infinity;
        Vector3 point = listSpawnTf[0].position;
        for(int i =0; i< listSpawnTf.Count; i++)
        {
            if(Vector3.Distance(listSpawnTf[i].position, player.tf.position)< distance)
            {
                point = listSpawnTf[i].position;
            }
        }
        return point;
    }


    public void SpawnNumberEnemy(Enemy enemyPrefab, float number)
    {
        Vector3 point = ChooseClosedPoint();
        SpawnAEnemy(enemyPrefab, point);
        for(int i =1; i< number; i++)
        {
            int index = Random.Range(0, listSpawnTf.Count);
            Vector3 pos = listSpawnTf[index].position;
            SpawnAEnemy(enemyPrefab, pos);
        }
    }


    // public void SpawnAEnemy(Enemy enemyPrefab)
    // {
        
    //     int VertexIndex = Random.Range(0, triangulation.vertices.Length);
    //     NavMeshHit Hit;
    //     if (NavMesh.SamplePosition(triangulation.vertices[VertexIndex], out Hit,  0.1f,  NavMesh.AllAreas))
    //     {
    //         if(Hit.mask>0.5)
    //         {
    //             Enemy enemy = Instantiate(enemyPrefab);
    //             enemy.agent.Warp(Hit.position);
    //             Debug.DrawRay(Hit.position, Vector3.up*100, Color.red, Mathf.Infinity);
    //             enemy.OnInit();
    //         }
            
            
    //     }
    // }
}
