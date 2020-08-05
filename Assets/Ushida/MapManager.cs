using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    //マップデータが存在するパス
    private const string MAP_DATA_PATH = "MapData/floorData";

    public MapData FloorData { set; get; }

    //座標とタイルの種類のディクショナリ
    private Dictionary<(int x, int z), FloorType> m_floorTypeDic = new Dictionary<(int x, int z) , FloorType>();
    public Dictionary<(int x, int z) , FloorType> FloorTypeDic { get { return m_floorTypeDic; } set { m_floorTypeDic = value; } }

    //スタートポジションとゴールポジションのディクショナリ
    public List<Vector3> StartPositionList { get ; set ; }
    public List<Vector3> GoalPositionList{ get ; set ; }

    //マップ生成が終わったかどうか
    public bool MapCreated { get; set; } = false;

    //プレイヤーのインスタンス
    [SerializeField] GameObject[] m_players;

    public enum FloorType
    {
        None = -1,
        Floor = 1,
        Wall,
        Water,
        Start,
        Goal,
    }

    protected override void Awake()
    {
        //スクリプタブルオブジェクトからマップデータを読み込んで初期化
        FloorData = Resources.Load<MapData>(MAP_DATA_PATH);

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

        int index = 0;
        foreach (var pos in StartPositionList)
        {
            Instantiate(m_players[index], MapCreate.CreatePosition + new Vector3(pos.x, 0, -pos.z), Quaternion.identity);
            PlayerMoveController pmc = m_players[index].GetComponent<PlayerMoveController>();
            pmc.SetPos((int)pos.x, (int)pos.z);

            index++;
            if (index >= m_players.Length)
            {
                break;
            }
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
