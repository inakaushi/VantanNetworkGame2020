using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    Rigidbody b_rigidy;
    [SerializeField] float b_speed;
    private Vector3 vector3;

    void Start()
    {
        b_rigidy = GetComponent<Rigidbody>();
    }

    void Update()
    {
        b_rigidy.velocity = vector3 * b_speed;
    }

    public void SetVec(Vector3 vector) 
    {
        vector3 = vector;
    }
}
