using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;

public class PalletManager : MonoBehaviour
{
    private int SelectSprite;   //パレットの選択ボタンナンバー

    //選択スプライトの数値を管理
    public int SelectProperty
    {
        get { return SelectSprite; }
        set { SelectSprite = value; }
    }

    private bool Clear;                           //スプライトをnullにするフラグ変数
    //オールクリア時の指示を各ボタンに送る
    public bool ACProperty
    {
        get { return Clear; }
        set { Clear = value; }
    }

    private int count;                            //フィールドボタンのカウント変数

    private int[] FieldPosition = new int[400];    //各ポジションのスプライトナンバー

    const string PATH = "Assets/Resources/MapData/FloorDate.csv";

    //オールクリアを押した処理
    public void AllClear()
    {
        ACProperty = true;
    }

    //各ボタンのクリアが出来たかカウントする
    public void SignCount()
    {
        count++;
        if (count == 400)
        {
            Array.Clear(FieldPosition, 0, FieldPosition.Length);
            ACProperty = false;
            count = 0;
        }
    }

    //各ポジションのスプライトナンバーを取得
    public void SetPosition(int pos)
    {
        FieldPosition[pos] = SelectSprite;
    }

    //フィールドデータをcsvに保存処理
    public void CSVSeve()
    {
        string lineText = null;
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/Resources/MapData/" + "FloorDate.csv");
        sw = fi.AppendText();

        for (int i = 0; i < 400; i++)
        {
            if (i % 20 == 0)
            {
                lineText = lineText + FieldPosition[i].ToString();
            }
            else
            {
                lineText = lineText + "," + FieldPosition[i].ToString();
            }

            if (i % 20 == 19)
            {
                sw.WriteLine(lineText);
                lineText = null;
            }
        }
        sw.Flush();
        sw.Close();
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
    }
}
