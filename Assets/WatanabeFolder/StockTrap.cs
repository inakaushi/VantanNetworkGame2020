using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockTrap : MonoBehaviour
{
    public List<SelectTrapDate> selectTrapList = new List<SelectTrapDate>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetTraps(string name) 
    {
        selectTrapList.Add(new SelectTrapDate(name));
        Debug.Log(name + "ゲット");
    } 
}
