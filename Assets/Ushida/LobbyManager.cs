using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    PhotonView m_view;

    bool ready;

    int maxMemberNum = 4;
    int memberNum = 0;

    [SerializeField] Text m_readyMemberNum;
    [SerializeField] GameObject m_gameStartButton;

    private void Start()
    {
        m_view = GetComponent<PhotonView>();
    }

    public void PushReadyButton()
    {
        if (!ready)
        {
            ready = true;
            object[] parameters = new object[] { true };
            m_view.RPC("SyncReadyMaster", RpcTarget.MasterClient, parameters);
        }
        else
        {
            ready = false;
            object[] parameters = new object[] { false };
            m_view.RPC("SyncReadyMaster", RpcTarget.MasterClient, parameters);
        }
        
    }

    [PunRPC]
    void SyncReadyMaster(bool flag)
    {
        if (flag)
        {
            memberNum++;
        }
        else
        {
            memberNum--;
        }
        object[] parameters = new object[] { memberNum };
        m_view.RPC("SyncReadyGuage", RpcTarget.All, parameters);

        if (memberNum == maxMemberNum)
        {
            m_gameStartButton.SetActive(true);
        }
    }

    [PunRPC]
    void SyncReadyGuage(int memberNum)
    {
        m_readyMemberNum.text = memberNum.ToString() + " / 4";
    }
}
