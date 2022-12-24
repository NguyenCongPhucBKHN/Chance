using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text level;

    private void Update()
    {
        level.text="Level: "+ Data.Instance.GetLevel().ToString();
    }

    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<MianMenu>();
        Close();
    }

    public void ReLoadLevel()
    {
        UIManager.Instance.OpenUI<MianMenu>();
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
        LevelManager.Instance.LoadLevel(Data.Instance.GetLevel());
        Close();
    }

    public void RePlayButton()
    {
        UIManager.Instance.OpenUI<MianMenu>();
        GameManagerr.Instance.ChangeState(EGameState.MainMenu);
        Data.Instance.SetLevel(1);
        LevelManager.Instance.LoadLevel(1);
        Close();

    }
}
