#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldButton : MonoBehaviour
{
    public int PosNum;　　　　　　　　　　　//各ボタンのシリアルナンバー
    public Sprite[] Pattern;                //各スプライト取得
    public GameObject palletManager;        //PalletManager取得

    private PalletManager palletM;          //PalletManagerキャッシュ変数

    private bool ACsign;                    //オールクリアのフラグ変数

    void Start()
    {
        PosNum = transform.GetSiblingIndex();

        palletM = palletManager.GetComponent<PalletManager>();
    }

    void Update()
    {
        SignalCheck();
    }

    //フィールドボタンを押した処理
    public void PushButton()
    {
        //マウスがクリックされてなければリターンする
        if (Input.GetMouseButton(0) == false)
        {
            return;
        }

        //選択中のパレットナンバーからスプライトを取得
        if (palletM.SelectProperty == 0)
        {
            //パレットナンバーが0ならスプライトを削除
            this.gameObject.GetComponent<Image>().sprite = null;
            palletM.SetPosition(PosNum);
        }
        else
        {
            //パレットナンバーにあったスプライトを表示する
            this.gameObject.GetComponent<Image>().sprite = Pattern[palletM.SelectProperty];
            palletM.SetPosition(PosNum);
        }
    }

    public void SetSprite(int num)
    {
        this.gameObject.GetComponent<Image>().sprite = Pattern[num];
    }

    //オールクリアのシグナルを判断する
    void SignalCheck()
    {
        //オールクリアの指示判断
        if (palletM.ACProperty && ACsign == false)
        {
            SpriteClear();
        }
        //オールクリア後の復旧判断
        if (palletM.ACProperty == false && ACsign)
        {
            ACsign = false;
        }
    }

    //オールクリア時の処理
    void SpriteClear()
    {
        this.gameObject.GetComponent<Image>().sprite = null;
        palletM.SignCount();
        ACsign = true;
    }
}
#endif