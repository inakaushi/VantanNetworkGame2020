using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Schema;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create MapData")]
public class MapData : ScriptableObject
{
    //public int[,] mapArray = new int[20, 20];
    //public int[,] MapArray { get => mapArray; set => mapArray = value; }

    //配列での座標と、Unity上での座標のDictionary
    [SerializeField]
    private List<Vector3> startPositionList = new List<Vector3>();
    public List<Vector3> StartPositionList { get => startPositionList; set => startPositionList = value; }

    [SerializeField]
    private List<Vector3> goalPositionList = new List<Vector3>();
    public List<Vector3> GoalPositionList { get => goalPositionList; set => goalPositionList = value; }

    public Array2D mapArray2D = new Array2D();
}
