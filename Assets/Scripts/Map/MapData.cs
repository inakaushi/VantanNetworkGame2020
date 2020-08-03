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

    [SerializeField]
    private List<Vector3> startPosition = new List<Vector3>();
    public List<Vector3> StartPosition { get => startPosition; set => startPosition = value; }

    [SerializeField]
    private List<Vector3> goalPosition = new List<Vector3>();
    public List<Vector3> GoalPosition { get => goalPosition; set => goalPosition = value; }

    public Array2D mapArray2D = new Array2D();
}
