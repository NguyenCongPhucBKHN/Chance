using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LevelManager : Singleton<LevelManager>
{
    public Level[] levelPrefabs;
    // public  Melee meleePrefab;
    // public Range rangePrefab;
    // public Boss bossPrefab;
    public EnemyDatas enemyDatas;
    public List<Enemy> enemies = new List<Enemy>();
    public Player player;
    public Vector3 savePointPlayer;
    public Level currentLevel;
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
    
    private void Awake() 
    {
        levelIndex = PlayerPrefs.GetInt("Level", 0);
        // currentLevel = levelPrefabs[0]; 
    }

    private void Start() 
    {
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<MainMenu>();
    

    }

    public void OnInit()
    {
        player.tf.position = savePointPlayer;
        player.OnInit();
        // currentLevel.OnInit();
        SpawnEnemyWhileInit();
    }

    public void LoadLevel(int index)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        else if(index > levelPrefabs.Length)
        {
            index = levelPrefabs.Length -1;
            
        }
        currentLevel = Instantiate(levelPrefabs[index]);
        currentLevel.OnInit();
    }

    public void OnStartGame()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
        //TODO: Check ChangeState Enemy
        for(int i =0; i< enemies.Count; i++)
        {
            enemies[i].ChangeState(new PatrolState());
        }

    }

    public void OnFinishGame()
    {   

        // OnDespawn();
        for(int i =0; i< enemies.Count; i++)
        {
            enemies[i].ChangeAnim("Idle");
            enemies[i].StopMoving();
            enemies[i].ChangeState(null);
            
        }

    }

    public void OnReset()
    {
        SimplePool.CollectAll();
        enemies.Clear();
        OnDespawn();
        OnDespawnCurrentStage();

    }

     public void OnRetry()
    {
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<MainMenu>();
    }

    internal void OnNextLevel()
    {
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<MainMenu>();
    }

    public void OnDespawn()
    {
        for(int i =0; i< enemies.Count; i++)
        {
            enemies[i].ResetTarget();
            SimplePool.Despawn(enemies[i]);
        }
        enemies.Clear();
        ResetCounter();
        // OnDespawnCurrentStage();
    }

    public void OnDespawnCurrentStage()
    {
        currentLevel.DespawnCurrentStage();
    }
    public void DespawnPrevStage()
    {
        currentLevel.DespawnPrevStage();
    }
    public void ResetCounter()
    {
        meleeDead =0;
        rangeDead =0;
        bossDead=0;
        meleeCouter=0;
        rangeCouter=0;
        bossCouter=0;
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
        SpawnEnemyAmount(EnemyType.Boss, 1);
    }

    public bool CheckSpawn(EnemyType enemyType)
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
            if(bossDead == TotalBoss)
            {

                currentLevel.currentStage.endStage.gameObject.SetActive(true);
                currentLevel.currentStage.endStage.NextStage();
                player.arrowTF.gameObject.SetActive(!currentLevel.IsEndLevel);
            }
            return  bossDead+ bossCouter < TotalBoss;
        }
    }
    public void SpawnWhileEnemyDead(EnemyType enemyType)
    {
        if(CheckSpawn(enemyType))
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
            Enemy enemy = SimplePool.Spawn<Enemy>(enemyPrefab, Paths[pathId].WayPoints[pointId].position, Quaternion.identity);
            enemy.path = Paths[pathId];
            IncreaseEnemyCount(enemyType);
            enemies.Add(enemy);
            enemy.OnInit();
        }
    }

    public  void IncreaseEnemyCount(EnemyType enemyType)
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

}
