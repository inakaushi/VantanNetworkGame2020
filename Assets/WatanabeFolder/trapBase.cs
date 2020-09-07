using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase
{
    private bool trapState = false;

    public void TrapOn() 
    {
        trapState = true;
    }

    public void TrapOff()
    {
        trapState = false;
    }

    public bool NowTrapState() 
    {
        return trapState;
    }
}
