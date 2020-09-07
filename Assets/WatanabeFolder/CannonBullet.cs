using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    Rigidbody b_rigidy;
    [SerializeField] float b_speed;

    void Start()
    {
        b_rigidy = GetComponent<Rigidbody>();
    }

    void Update()
    {
        b_rigidy.velocity = transform.forward * b_speed;
    }
}
