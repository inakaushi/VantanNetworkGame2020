using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyButton : MonoBehaviour
{
    [SerializeField] GameObject text_StandBy;
    [SerializeField] GameObject text_Ready;
    bool ready = false;
    
    /// <summary>ボタンをクリックした時</summary>
    public void OnClick()
    {
        //ボタンのテキストを準備完了(ready)にする
        if (!ready)
        {
            text_Ready.SetActive(false);
            text_StandBy.SetActive(true);
            ready = true;
        }
        //ボタンのテキストを待機中(standby)にする
        else
        {
            text_Ready.SetActive(true);
            text_StandBy.SetActive(false);
            ready = false;
        }
    }
}
