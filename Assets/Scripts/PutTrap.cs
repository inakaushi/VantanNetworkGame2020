using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutTrap : MonoBehaviour
{
    [SerializeField] Text m_trapText;

    GameObject m_trap;
    GameObject m_tempYesTrap;
    GameObject m_tempNoTrap;

    //仮配置トラップを格納するための変数
    GameObject go;

    //1フレーム前にポイントしていたタイルのポジション
    Vector3? Position = null;

    Ray ray;

    void Update()
    {
        if (m_trap)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.tag == "Ground")
                    {
                        //仮配置の罠を削除
                        Destroy(go);

                        Destroy(hit.collider.gameObject);

                        Instantiate(m_trap, hit.transform.position, Quaternion.identity);

                        //トラップ情報を初期化する
                        ClearTrap();
                        Position = null;
                    }
                }
            }
            else
            {
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    //前フレームと同じオブジェクトを指していたら、何もしない
                    if (Position == hit.transform.position)
                    {
                        return;
                    }

                    //前フレームと違うオブジェクトを指していた場合、仮配置トラップを削除
                    if (Position != null)
                    {
                        Position = null;
                        Destroy(go);
                    }

                    //タグが床、またはトラップだった場合、配置可能仮トラップ生成
                    if (hit.collider.tag == "Ground" || hit.collider.tag == "Trap")
                    {
                        if (Position == null)
                        {
                            Position = hit.transform.position;
                            go = Instantiate(m_tempYesTrap, hit.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        }
                    }
                    //タグが配置不可能だった場合、配置不可能仮トラップ生成
                    else if (hit.collider.tag == "CantPut")
                    {
                        if (Position == null)
                        {
                            Position = hit.transform.position;
                            go = Instantiate(m_tempNoTrap, hit.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

    public void SetTrap(string trap)
    {
        if (PhaseManager.instance.GetPhase() != PhaseManager.PHASE.SET_TRAP)
        {
            return;
        }

        MapManager.Instance.PlayerCamera.StartTrapSetMode();
        m_trap = Resources.Load(trap) as GameObject;
        m_tempYesTrap = Resources.Load(trap + "TempYes") as GameObject;
        m_tempNoTrap = Resources.Load(trap + "TempNo") as GameObject;

        m_trapText.text = "Trap : " + m_trap.name;
    }

    void ClearTrap()
    {
        m_trap = null;
        m_tempYesTrap = null;
        m_tempNoTrap = null;

        m_trapText.text = "Trap : Nothing";
    }

    public void EndSetTrap()
	{
        PhaseManager.instance.EndSetTrap();
        MapManager.Instance.PlayerCamera.SetTarget(MapManager.Instance.Player.transform);
	}
}
