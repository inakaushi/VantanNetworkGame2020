using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject point;
    [SerializeField] GameObject bullet;
    [SerializeField] float firindSpan = 0;
    float firingtime = 0;

    private void Update()
    {
        firingtime += Time.deltaTime;
        if (firingtime >= firindSpan)
        {
            Instantiate(bullet, point.transform.position, Quaternion.identity);
            firingtime = 0;
        }
    }
}
