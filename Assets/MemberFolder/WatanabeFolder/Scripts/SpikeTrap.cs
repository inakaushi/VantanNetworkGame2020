using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    TrapBase trapBase;
    Animator spikeanimetor;

    private void Awake()
    {
        trapBase = new TrapBase();
        spikeanimetor = GetComponent<Animator>();
    }

    public void NeedleOn() 
    {
        trapBase.TrapOn(); 
    }

    public void NeedleOff() 
    {
        trapBase.TrapOff();
    }

    public bool NowNeedleState() 
    {
        return trapBase.NowTrapState();
    }
}
