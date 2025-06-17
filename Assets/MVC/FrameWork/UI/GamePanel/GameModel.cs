using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : ModelBase
{
    public override void Init(UIWindow uiBase)
    {
        Debug.Log("GameModel Init");
    }

    public override void OnEnable()
    {
        Debug.Log("GameModel OnEnable");
    }

    public override void OnDestory()
    {
        Debug.Log("GameModel OnDestory");
    }
}
