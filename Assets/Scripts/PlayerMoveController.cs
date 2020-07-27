using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] float m_timePerTile;

    bool isOnTileCenter = true;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //移動方向を正規化
        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        if (!isOnTileCenter)
        {
            return;
        }

        if (moveDir.x > 0.5f)
        {
            transform.forward = Vector3.right;
            Walk();
        }
        else if (moveDir.x < -0.5f)
        {
            transform.forward = -Vector3.right;
            Walk();
        }
        else if (moveDir.z > 0.5f)
        {
            transform.forward = Vector3.forward;
            Walk();
        }
        else if (moveDir.z < -0.5f)
        {
            transform.forward = -Vector3.forward;
            Walk();
        }
    }

    void Walk()
    {
        isOnTileCenter = false;
        transform.DOLocalJump(transform.forward, 0.5f, 1, m_timePerTile).SetRelative().OnComplete(() => isOnTileCenter = true);
    }
}
