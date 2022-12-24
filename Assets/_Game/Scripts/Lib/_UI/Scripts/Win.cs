using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public Text level;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        level.text="Level: "+ Data.Instance.GetLevel().ToString();
    }
    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<MianMenu>();
        Close();
    }

    public void RePlayButton()
    {
        UIManager.Instance.OpenUI<MianMenu>();
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
        LevelManager.Instance.LoadLevel(1);
        Data.Instance.SetLevel(1);
        Close();

    }

    public void LoadNextLevel()
    {
        UIManager.Instance.OpenUI<MianMenu>();
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
        LevelManager.Instance.LoadLevel(Data.Instance.GetNextLevel());
        Close();
    }

}
