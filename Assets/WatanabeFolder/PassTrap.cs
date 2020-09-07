using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTrap : MonoBehaviour
{
    [SerializeField] private string trapName;
    [SerializeField] private GameObject SelectTrapManager;
    StockTrap destination;

    void Start()
    {
        destination = SelectTrapManager.GetComponent<StockTrap>();
    }

    void Update()
    {
        
    }

    public void HandOverTrap() 
    {
        destination.SetTraps(trapName);
    }
}
