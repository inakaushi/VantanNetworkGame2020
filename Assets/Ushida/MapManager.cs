using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private MapData floorData;
    public MapData FloorData { get { return floorData; } }

    //座標とタイルの種類のディクショナリ
    private Dictionary<(int x, int z), FloorType> m_floorTypeDic = new Dictionary<(int x, int z) , FloorType>();
    public Dictionary<(int x, int z) , FloorType> FloorTypeDic { get { return m_floorTypeDic; } set { m_floorTypeDic = value; } }

    //スタートポジションとゴールポジションのディクショナリ
    public List<Vector3> StartPositionList { get ; set ; }
    public List<Vector3> GoalPositionList{ get ; set ; }

    //マップ生成が終わったかどうか
    public bool MapCreated { get; set; } = false;

    /// <summary>プレイヤーのプレハブ</summary>
    [SerializeField] string m_playerPrefabName = "Player";

    //プレイヤーのインスタンス
    public GameObject Player { get; set; }

    [SerializeField] CameraController m_playerCamera;
    public CameraController PlayerCamera { get { return m_playerCamera; } }

    public enum FloorType
    {
        None = 0,
        Floor,
        Wall,
        Water,
        Start,
        Goal,
    }

    //シングルトン用処理--------------------------------------------------------------------------------
    [SerializeField]
    private bool dontDestroyOnLoad = false;
    private static MapManager instance;
    public static MapManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (MapManager)FindObjectOfType(typeof(MapManager));
                if (instance == null)
                {
                    Debug.LogError(typeof(MapManager) + " をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }
    //---------------------------------------------------------------------------------------------------

    private void Awake()
    {
        //シングルトン用処理----------------------------------------------------------------------
        // 他のGameObjectにアタッチされているか調べる.
        // アタッチされている場合は破棄する.
        if (this != Instance)
        {
            Destroy(this);
            //Destroy(this.gameObject);
            Debug.LogWarning(
                typeof(MapManager) +
                " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
            return;
        }

        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        //-----------------------------------------------------------------------------------------

        //マップデータを読み込む---------------------------------------------
        for (int i = 0; i < FloorData.mapArray2D.Height; i++)
        {
            for (int j = 0; j < FloorData.mapArray2D.Width; j++)
            {
                int num = FloorData.mapArray2D.Get(j, i);
                m_floorTypeDic.Add((j, i), (FloorType)num);
            }
        }

        StartPositionList = FloorData.StartPositionList;
        GoalPositionList = FloorData.GoalPositionList;
    }

    public void CreatePlayer(int index)
    {
        Vector3 spawnPos = MapCreate.CreatePosition + new Vector3(StartPositionList[index].x, 0, -StartPositionList[index].z);
        Player = PhotonNetwork.Instantiate(m_playerPrefabName, spawnPos, Quaternion.identity);
        PlayerMoveController pmc = Player.GetComponent<PlayerMoveController>();
        pmc.SetPos((int)StartPositionList[index].x, (int)StartPositionList[index].z);

        m_playerCamera.SetTarget(Player.transform);

        if (NetworkGameManager.Instance.Scene == NetworkGameManager.GameScene.Lobby)
        {
            if (SceneManager.GetActiveScene().buildIndex == (int)NetworkGameManager.GameScene.Game)
            {
                PhaseManager.instance.EndLoad();
                return;
            }
            PhaseManager.instance.LobbyStart();
        }
        else if (NetworkGameManager.Instance.Scene == NetworkGameManager.GameScene.Game)
        {
            PhaseManager.instance.EndLoad();
        }
    }

    public FloorType GetFloorType(int x, int z)
    {
        if (m_floorTypeDic.ContainsKey((x, z)))
        {
            return m_floorTypeDic[(x, z)];
        }
        else
        {
            return FloorType.None;
        }
    }
}
