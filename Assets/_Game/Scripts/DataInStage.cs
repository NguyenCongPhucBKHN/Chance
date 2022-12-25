using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "DataInStage", menuName = "Chance/DataInStage", order = 0)]
public class DataInStage : ScriptableObject 
{
public int totalMelee;
  public int totalRange;
  public int totalBoss;
  public int numberMelee;
  public int numberRange;
  public int numberBoss;
  public int enemySpawnInterval;

}

