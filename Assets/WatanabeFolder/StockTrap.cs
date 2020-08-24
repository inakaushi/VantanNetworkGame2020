using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockTrap : MonoBehaviour
{
    public List<SelectTrapDate> selectTrapList = new List<SelectTrapDate>(2);
    private int i = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetTraps(string name) 
    {
        if (!(i == 2))
        {
            selectTrapList.Add(new SelectTrapDate(name));
            Debug.Log(name + "ゲット");
            i++;
        }
        else
        {
            Debug.Log("これ以上はいりません");
        }
    }

    public int Times() 
    {
        return i;
    }

    public SelectTrapDate SelectTrapDate(int a) 
    {
        return selectTrapList[a];
    }
}
