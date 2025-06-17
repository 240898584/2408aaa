using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : ViewBase
{
    Button startGameBtn;
    public override void Init(UIWindow uiBase)
    {
        Debug.Log("GameView Init");
        startGameBtn = uiBase.transform.Find("StartBtn").GetComponent<Button>();
        startGameBtn.onClick.AddListener(() =>
        {
            (uiBase.control as GameControl).ChangeScene();
        });
    }

    public override void OnEnable()
    {
        Debug.Log("GameView OnEnable");
    }

    public override void OnDestory()
    {
    }

    public override void Update()
    {
    }
}
