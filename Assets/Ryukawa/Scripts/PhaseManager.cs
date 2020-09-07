using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
	#region Singleton
	[SerializeField] bool dontDestroyOnLoad = false;
	public static PhaseManager instance;

	public static PhaseManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (PhaseManager)FindObjectOfType(typeof(PhaseManager));

				if (instance == null)
				{
					Debug.LogError("PhaseManager をアタッチしているGameObjectはありません");
				}
			}

			return instance;
		}
	}
	#endregion

	public enum PHASE
	{
        LOAD,
		LOBBY,
        SET_TRAP,
        PLAYER_MOVE,
	}

	static PHASE phase = PHASE.LOAD;

	void Awake()
	{
		#region Singleton
		if (this != Instance)
		{
			Destroy(gameObject);
			Debug.LogWarning(
				typeof(PhaseManager) +
				" は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
				" アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
			return;
		}

		if (dontDestroyOnLoad)
		{
			DontDestroyOnLoad(gameObject);
		}
		#endregion

		phase = PHASE.LOAD;
	}

	public PHASE GetPhase()
	{
		return phase;
	}

	public void EndLoad()
	{
		phase = PHASE.SET_TRAP;
	}

	public void EndSetTrap()
	{
		phase = PHASE.PLAYER_MOVE;
	}

	public void LobbyStart()
    {
		phase = PHASE.LOBBY;
    }
}
