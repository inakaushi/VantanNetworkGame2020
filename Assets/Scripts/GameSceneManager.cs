using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class GameSceneManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //もし部屋に参加していなかったら何もしない
        if (NetworkGameManager.Instance.JoinedRoom)
        {
            int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber - 1;
            MapManager.Instance.CreatePlayer(actorNumber);
        }
    }

    
}
