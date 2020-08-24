using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    [SerializeField] GameObject m_ground;
    [SerializeField] GameObject m_wall;
    [SerializeField] GameObject m_water;
    [SerializeField] GameObject m_start;
    [SerializeField] GameObject m_goal;

    public static Vector3 CreatePosition { get; set; } = new Vector3(0.5f, 0, -0.5f);
    int posX = 0;
    int posZ = 0;

    private void Start()
    {
        StartCoroutine(MapCreateCoruetine());
    }

    IEnumerator MapCreateCoruetine()
    {
        yield return null;

        for (int i = 0; i < MapManager.Instance.FloorData.mapArray2D.Height; i++)
        {
            for (int j = 0; j < MapManager.Instance.FloorData.mapArray2D.Width; j++)
            {
                posZ = i;
                posX = j;
                MapManager.FloorType type = MapManager.Instance.FloorTypeDic[(j, i)];

                switch (type)
                {
                    case MapManager.FloorType.Floor:
                        Instantiate(m_ground, CreatePosition + new Vector3(posX, 0, -posZ), Quaternion.identity);
                        break;
                    case MapManager.FloorType.Wall:
                        Instantiate(m_wall, CreatePosition + new Vector3(posX, 1, -posZ), Quaternion.identity);
                        break;
                    case MapManager.FloorType.Water:
                        Instantiate(m_water, CreatePosition + new Vector3(posX, 0, -posZ), Quaternion.identity);
                        break;
                    case MapManager.FloorType.Start:
                        Instantiate(m_start, CreatePosition + new Vector3(posX, 0, -posZ), Quaternion.identity);
                        break;
                    case MapManager.FloorType.Goal:
                        Instantiate(m_goal, CreatePosition + new Vector3(posX, 0, -posZ), Quaternion.identity);
                        break;
                }
            }
        }

        yield return null;

        MapManager.Instance.MapCreated = true;

        if (NetworkGameManager.Instance.enabled == false)
        {
            NetworkGameManager.Instance.enabled = true;
        }
    }
}
