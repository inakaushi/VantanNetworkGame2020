using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadnextScene : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    StockTrap stockTrap;
    void Start()
    {
        stockTrap = GameObject.Find("Maneger").GetComponent<StockTrap>();
    }

    void Update()
    {
        
    }

    public void Confirm() 
    {
        if (stockTrap.Times() == 2)
        {
            SceneManager.sceneLoaded += SceneLoad;

            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void SceneLoad(Scene scene, LoadSceneMode mode) 
    {
        var trapHolder = GameObject.Find("T").GetComponent<TrapHolder>();

        for (int i = 0; i < stockTrap.Times(); i++)
        {
            trapHolder.TrapSet(stockTrap.SelectTrapDate(i));
        }

        SceneManager.sceneLoaded -= SceneLoad;
    }
}
