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
    public int enemyAmount = 4; //TODO: Get from currentLevel;
    public List<Transform> listSpawnTf => currentLevel.listSpawnEnemyTf;
    public EnemyPath[] Paths => currentLevel.paths;
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

    private int   meleeSpawned;
    private int   rangeSpawned;
    private int   bossSpawned;

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
        StartCoroutine(IESpawnsEnemies());

    }

    public void OnInit()
    {
        player.tf.position = savePointPlayer;
        player.OnInit();
        
        
    }

    //Cho spawn
    private IEnumerator IESpawnsEnemies()
    {
       
        if(meleeSpawned<TotalMelee)
        {
            yield return StartCoroutine(IESpawnsEnemie(amoutCurrentMelee, NumberMelee,  meleePrefab , Paths[0]));
            amoutCurrentMelee ++;
            meleeSpawned++;
        }
        if(rangeSpawned<TotalMelee)
        {
            yield return StartCoroutine(IESpawnsEnemie( amountCurrentRange, NumberRange, rangePrefab , Paths[0]));
            amountCurrentRange++;
            rangeSpawned++;
        }
        if(meleeSpawned==TotalMelee && rangeSpawned==TotalRange && bossSpawned< NumberBoss)
        {
            yield return StartCoroutine(IESpawnsEnemie( amountCurrentBoss, NumberBoss, bossPrefab , Paths[0]));
            amountCurrentBoss++;
            bossSpawned++;
        }
    }

    //Spawn 1 loai
   private IEnumerator  IESpawnsEnemie( int  amoutcurrent, float  amoutTotal, Enemy enemyPrefab ,EnemyPath path)
    {
        if(amoutcurrent< amoutTotal)
        {
            yield return new WaitForSeconds(EnemySpawnInterval); //TODO: speed
            Enemy enemy = Instantiate(enemyPrefab, path.WayPoints[0].position, Quaternion.identity);
            enemy.OnInit();
            // amoutcurrent ++;
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
