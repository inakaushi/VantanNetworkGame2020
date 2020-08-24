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
        transform.localEulerAngles = angle;
    }

	void LateUpdate()
	{
        CameraMove(Time.deltaTime);
	}

    void CameraMove(float dt)
	{
		if (target)
		{
			var pos = target.transform.position + offsetPos;
			transform.localPosition = Vector3.Lerp(transform.localPosition, pos, speed * dt);
		}
	}

    public void SetTarget(Transform target)
	{
        this.target = target;
	}
}
