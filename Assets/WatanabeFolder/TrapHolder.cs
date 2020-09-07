using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHolder : MonoBehaviour
{
    private List<SelectTrapDate> traps = new List<SelectTrapDate>(2);

    void Start()
    {
        traps.ForEach(Debug.Log);
    }

    void Update()
    {
        
    }

    public void TrapSet(SelectTrapDate selectTrapDate) 
    {
        traps.Add(selectTrapDate);
    }
}
