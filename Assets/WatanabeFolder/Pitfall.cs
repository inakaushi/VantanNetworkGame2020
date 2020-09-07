using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitfall : MonoBehaviour
{
    TrapBase trapBase;
    Animator pitFallanimator;
    [SerializeField] float pitSwitchOnTime;
    float pitOnUntil = 0;
    private bool trapActivation = false;

    private void Awake()
    {
        trapBase = new TrapBase();
        pitFallanimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (trapBase.NowTrapState()) { }

        if (trapActivation && !trapBase.NowTrapState())
        {
            pitOnUntil += Time.deltaTime;
            if (pitOnUntil >= pitSwitchOnTime)
            {
                pitFallanimator.SetTrigger("ON");
                trapBase.TrapOn();
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        trapActivation = true;

    }

    public bool NowPitState() 
    {
        return trapBase.NowTrapState();
    }
}
