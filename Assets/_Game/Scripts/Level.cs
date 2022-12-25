using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
  internal Stage currentStage;
  public Stage[] stages;
  public int currentStageIndex;
  public void OnInit()
  {
    currentStageIndex=0;
    currentStage = stages[currentStageIndex];
  }
  public void ChangeStage()
  {
    currentStageIndex ++;
    currentStage = stages[currentStageIndex];
  }
  

}
