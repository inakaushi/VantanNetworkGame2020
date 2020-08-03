using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 offsetPos = Vector3.zero;
    [SerializeField] Vector3 angle = Vector3.zero;
    [SerializeField] float speed = 5f;

    Transform target = null;

    void Start()
    {
        target = GameObject.Find("Player").transform;

        transform.localEulerAngles = angle;
        CameraMove(1000f);
    }

	void LateUpdate()
	{
        CameraMove(Time.deltaTime);
	}

    void CameraMove(float dt)
	{
        var pos = target.transform.position + offsetPos;
        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, speed * dt);
	}
}
