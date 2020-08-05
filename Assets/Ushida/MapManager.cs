using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonMonoBehaviour<MapManager>
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

    //プレイヤーのプレハブ
    [SerializeField] GameObject m_playerPrefab;

    //プレイヤーのインスタンス
    public GameObject Player { get; set; }

    [SerializeField] CameraController m_playerCamera;

    public enum FloorType
    {
        None = 0,
        Floor,
        Wall,
        Water,
        Start,
        Goal,
    }

    protected override void Awake()
    {
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

    public void CreatePlayer()
    {
        if (!MapCreated)
        {
            return;
        }

        foreach (var pos in StartPositionList)
        {
            Player = Instantiate(m_playerPrefab, MapCreate.CreatePosition + new Vector3(pos.x, 0, -pos.z), Quaternion.identity);
            PlayerMoveController pmc = Player.GetComponent<PlayerMoveController>();
            pmc.SetPos((int)pos.x, (int)pos.z);

            break;
        }

        m_playerCamera.enabled = true;
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
