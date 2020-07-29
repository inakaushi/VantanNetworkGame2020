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

    [SerializeField] MapData m_floorData;

    Vector3 createPosition = new Vector3(0.5f, 0, 0.5f);
    int posX = 0;
    int posZ = 0;

    private void Start()
    {
        StartCoroutine(MapCreateCoruetine());
    }

    IEnumerator MapCreateCoruetine()
    {
        yield return null;

        for (int i = 0; i < m_floorData.mapArray2D.Height; i++)
        {
            for (int j = 0; j < m_floorData.mapArray2D.Width; j++)
            {
                posZ = i;
                posX = j;
                int num = m_floorData.mapArray2D.Get(i, j);

                switch (num)
                {
                    case 1:
                        Instantiate(m_ground, createPosition + new Vector3(posX, 0, -posZ), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(m_wall, createPosition + new Vector3(posX, 1, -posZ), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(m_water, createPosition + new Vector3(posX, 0, -posZ), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(m_start, createPosition + new Vector3(posX, 0, -posZ), Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(m_goal, createPosition + new Vector3(posX, 0, -posZ), Quaternion.identity);
                        break;
                }

                yield return null;
            }
        }
    }
}
