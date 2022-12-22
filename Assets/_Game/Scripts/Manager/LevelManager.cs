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
    public float EnemySpawnInterval => currentLevel.enemySpawnInterval;
    // private NavMeshTriangulation triangulation;
    private Level currentLevel;
    private int levelIndex;

    #region  Variable parameter enemy in level
    public float TotalMelee => currentLevel.totalMelee;
    public float TotalRange => currentLevel.totalRange;
    public float TotalBoss => currentLevel.totalBoss;
    public float NumberMelee => currentLevel.numberMelee;
    public float NumberRange => currentLevel.numberRange;
    public float NumberBoss => currentLevel.numberBoss;
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
        int totalEnemies =2;
        yield return StartCoroutine(IESpawnsEnemie(  totalEnemies,  meleePrefab , Paths[0]));
    }

    //Spawn 1 loai
   private IEnumerator  IESpawnsEnemie( int totalEnemies, Enemy enemyPrefab ,EnemyPath path)
    {
        for(int i =0; i< totalEnemies; i++)
        {
            yield return new WaitForSeconds(EnemySpawnInterval/2); //TODO: speed
            Enemy enemy = Instantiate(enemyPrefab, path.WayPoints[0].position, Quaternion.identity);
            enemy.OnInit();
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
