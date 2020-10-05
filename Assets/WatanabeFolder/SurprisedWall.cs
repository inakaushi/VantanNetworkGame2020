using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurprisedWall : MonoBehaviour
{
    [SerializeField] CapsuleCollider m_co;
    TrapBase trapBase;
    private void Awake()
    {
        trapBase = new TrapBase();
    }

    private void Update()
    {
        
    }

    public void ColliderON() 
    {
        m_co.enabled = true;
        trapBase.TrapOn();
    }

    public void ColliderOff() 
    {
        trapBase.TrapOff();
        m_co.enabled = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        NowWallState();
    }

    private bool NowWallState() 
    {
        return trapBase.NowTrapState();
    }
}
