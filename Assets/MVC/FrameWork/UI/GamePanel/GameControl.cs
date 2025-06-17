using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : ControlBase
{
    public void ChangeScene()
    {
        GameScenesManager.Instance.LoadSceneAsync("Game", "NonePanel");
    }
    public override void Init(UIWindow uiBase)
    {
        Debug.Log("GameControl Init");
    }

    public override void OnEnable()
    {
        Debug.Log("GameControl OnEnable");
    }

    public override void OnDestory()
    {
        Debug.Log("GameControl OnDestory");
    }
}
