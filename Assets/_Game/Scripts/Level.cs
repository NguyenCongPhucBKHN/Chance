using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
  internal Stage currentStagePrefab;
  public Stage currentStage; //TODO internal
  public Stage[] stagesPrefab;
  public int currentStageIndex;
  public bool IsEndLevel => currentStageIndex >= stagesPrefab.Length-1;
  public Stage prevStage;
 
  public void OnInit()
  {
    currentStageIndex=0;
    currentStagePrefab = stagesPrefab[currentStageIndex];
    currentStage = Instantiate(currentStagePrefab);
  }
  public void DespawnPrevStage()
  {
    Invoke(nameof(DespawnStage), 2f);
  }

  public void DespawnStage()
  {
    Destroy(prevStage.gameObject);
  }
  
  public void SpawnNewStage()
  {
    currentStageIndex ++;
    if(currentStageIndex < stagesPrefab.Length)
    {
      currentStagePrefab = stagesPrefab[currentStageIndex];
      currentStage = Instantiate(currentStagePrefab);
    }
    else
    {
      GameManager.Instance.ChangeState(GameState.Pause);
      UIManager.Instance.CloseUI<Gameplay>();
      UIManager.Instance.OpenUI<Victory>();
    }
    
  }

}
