using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyDatas", menuName = "Chance/EnemyDatas", order = 0)]
public class EnemyDatas : ScriptableObject 
{
    public List<EnemyData> listData;
    public Enemy GetPrefabEnemy(EnemyType enemyt)
    {
        for(int i =0; i< listData.Count; i++)
        {
            if(listData[i].enemyType == enemyt)
            {
                return listData[i].enemyPrefab;
            }
        }
        return null;
    }
}

