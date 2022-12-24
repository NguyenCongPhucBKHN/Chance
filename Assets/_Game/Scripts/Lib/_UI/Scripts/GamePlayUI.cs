using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : UICanvas
{
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    [SerializeField] GameObject settingBtn;
    private void Update()
    {
        if(GameManagerr.Instance.IsState(EGameState.Finish))
        {
            Hide();
        }

        if(GameManagerr.Instance.IsState(EGameState.GamePlay))
        {
            Show();
        }
    }
    public void SettingBtn()
    {
     GameManagerr.Instance.ChangeState(EGameState.Pause);
     UIManager.Instance.OpenUI<Setting>();
     Close();   
    }

    void Hide()
    {
        settingBtn.SetActive(false);
    }

    void Show()
    {
        settingBtn.SetActive(true);
    }


}
