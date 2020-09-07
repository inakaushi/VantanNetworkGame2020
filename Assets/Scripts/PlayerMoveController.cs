using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMoveController : MonoBehaviourPunCallbacks
{
    [SerializeField] float m_timePerTile;

    bool isOnTileCenter = true;

    public int playerPosX = 0;
    public int playerPosZ = 0;

    //次に移動するマスのタイプ
    MapManager.FloorType m_nextFloorType = MapManager.FloorType.None;

    PhotonView m_view;

    private void Start()
    {
        m_view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!m_view.IsMine)
        {
            return;
        }

		if (PhaseManager.instance.GetPhase() != PhaseManager.PHASE.PLAYER_MOVE && PhaseManager.instance.GetPhase() != PhaseManager.PHASE.LOBBY)
		{
            return;
		}

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(h) < 0.2f)
        {
            h = 0;
        }

        if (Mathf.Abs(v) < 0.2f)
        {
            v = 0;
        }
        //移動方向を正規化
        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        if (!isOnTileCenter)
        {
            return;
        }

        //右に移動
        if (moveDir.x > 0.5f)
        {
            transform.forward = Vector3.right;

            m_nextFloorType = MapManager.Instance.GetFloorType(playerPosX + 1, playerPosZ);
            if (!(m_nextFloorType == MapManager.FloorType.Floor || m_nextFloorType == MapManager.FloorType.Start || m_nextFloorType == MapManager.FloorType.Goal)) return;

            playerPosX++;

            Walk();
        }
        //左に移動
        else if (moveDir.x < -0.5f)
        {
            transform.forward = -Vector3.right;

            m_nextFloorType = MapManager.Instance.GetFloorType(playerPosX - 1, playerPosZ);
            if (!(m_nextFloorType == MapManager.FloorType.Floor || m_nextFloorType == MapManager.FloorType.Start || m_nextFloorType == MapManager.FloorType.Goal)) return;

            playerPosX--;

            Walk();
        }
        //上に移動
        else if (moveDir.z > 0.5f)
        {
            transform.forward = Vector3.forward;

            m_nextFloorType = MapManager.Instance.GetFloorType(playerPosX, playerPosZ - 1);
            if (!(m_nextFloorType == MapManager.FloorType.Floor || m_nextFloorType == MapManager.FloorType.Start || m_nextFloorType == MapManager.FloorType.Goal)) return;

            playerPosZ--;

            Walk();
        }
        //下に移動
        else if (moveDir.z < -0.5f)
        {
            transform.forward = -Vector3.forward;

            m_nextFloorType = MapManager.Instance.GetFloorType(playerPosX, playerPosZ + 1);
            if (!(m_nextFloorType == MapManager.FloorType.Floor || m_nextFloorType == MapManager.FloorType.Start || m_nextFloorType == MapManager.FloorType.Goal)) return;

            playerPosZ++;

            Walk();
        }
    }

    void Walk()
    {
        isOnTileCenter = false;
        transform.DOLocalJump(transform.forward, 0.5f, 1, m_timePerTile).SetRelative().OnComplete(() => isOnTileCenter = true);
    }

    public void SetPos(int x, int z)
    {
        playerPosX = x;
        playerPosZ = z;
    }
}
