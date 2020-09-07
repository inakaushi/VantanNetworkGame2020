using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] string loadSceneName = null;
    [SerializeField] Button firstSelectButton = null;

    void Start()
    {
        firstSelectButton.Select();
    }

    void Update()
    {

    }

    public void JoinLobby()
	{
        SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Single);
    }

    public void Quit()
	{
        Debug.Log("Quit");
        Application.Quit();
	}
}
