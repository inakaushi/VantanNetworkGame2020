using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulletButton : MonoBehaviour
{
    public GameObject[] Marker;　　　　　　　//各マーカーオブジェクト取得
    public GameObject palletManager;         //PalletManagerオブジェクト取得

    private PalletManager palletM;           //PalletManagerのキャッシュ変数


    // Start is called before the first frame update
    void Start()
    {
        palletM = palletManager.GetComponent<PalletManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //パレットボタンを押した時の処理
    public void SelectButton(int num)
    {
        //選択中のナンバーと押したナンバーを比較
        if (num == palletM.SelectProperty)
        {
            //ナンバーが同じならMarkerをfalseして0を登録
            Marker[num].SetActive(false);
            palletM.SelectProperty = 0;

            //選択中のナンバーが0か確認
        }
        else if (palletM.SelectProperty == 0)
        {
            //0なら押したナンバーのMarkerをtrueして登録
            Marker[num].SetActive(true);
            palletM.SelectProperty = num;
        }
        else
        {
            //選択中のMarkerをfalseして新規選択のMarkerをtrueして登録
            Marker[palletM.SelectProperty].SetActive(false);
            Marker[num].SetActive(true);
            palletM.SelectProperty = num;
        }
    }
}
