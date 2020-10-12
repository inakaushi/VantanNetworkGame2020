using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	enum CAMERA_CONTROL_MODE
	{
		TARGET_PLAYER,
		TRAP_SET,
	}

	CAMERA_CONTROL_MODE controlType = CAMERA_CONTROL_MODE.TARGET_PLAYER;

	[SerializeField] float widthMoveCamera = 10f;
    [SerializeField] Vector3 offsetPos = Vector3.zero;
    [SerializeField] Vector3 angle = Vector3.zero;
    [SerializeField] float speed = 5f;
	[SerializeField] float trapSetModeMoveSpeed = 10f;

    Transform target = null;

    void Start()
    {
        transform.localEulerAngles = angle;
    }

	void LateUpdate()
	{
		switch (controlType)
		{
			case CAMERA_CONTROL_MODE.TARGET_PLAYER:
				TargetPlayerMove(Time.deltaTime);
				break;
			case CAMERA_CONTROL_MODE.TRAP_SET:
				TrapSetCameraMove(Time.deltaTime);
				break;
			default:
				break;
		}
	}

    void TargetPlayerMove(float dt)
	{
		if (target)
		{
			var pos = target.transform.position + offsetPos;
			transform.localPosition = Vector3.Lerp(transform.localPosition, pos, speed * dt);
		}
	}

	void TrapSetCameraMove(float dt)
	{
		var mousePos = Input.mousePosition;
		var pos = transform.localPosition;
		if (mousePos.x >= Screen.width - widthMoveCamera)
		{
			pos += Vector3.right; 
		}
		if (mousePos.x <= 0 + widthMoveCamera)
		{
			pos += Vector3.left;
		}
		if (mousePos.y >= Screen.height - widthMoveCamera)
		{
			pos += Vector3.forward;
		}
		if (mousePos.y <= 0 + widthMoveCamera)
		{
			pos += Vector3.back;
		}
		transform.localPosition = Vector3.Lerp(transform.localPosition, pos, speed * dt);
	}

    public void SetTarget(Transform target)
	{
		controlType = CAMERA_CONTROL_MODE.TARGET_PLAYER;
        this.target = target;
	}

	public void StartTrapSetMode()
	{
		if (controlType != CAMERA_CONTROL_MODE.TRAP_SET)
			controlType = CAMERA_CONTROL_MODE.TRAP_SET;
	}
}
