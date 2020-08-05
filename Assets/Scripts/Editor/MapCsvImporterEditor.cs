using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(MapCsvImporter))]
public class MapCsvImporterEditor : Editor
{
	public override void OnInspectorGUI()
	{
		var csvImporter = target as MapCsvImporter;
		DrawDefaultInspector();

		if (GUILayout.Button("マップデータの作成"))
		{
			Debug.Log("マップデータの作成ボタンが押された");
			SetCsvDataToScriptableObject(csvImporter);
		}
	}

	void SetCsvDataToScriptableObject(MapCsvImporter csvImporter)
	{
		// ボタンを押されたらパース実行
		if (csvImporter.csvFile == null)
		{
			Debug.LogWarning(csvImporter.name + " : 読み込むCSVファイルがセットされていません。");
			return;
		}

		// csvファイルをstring形式に変換
		string csvText = csvImporter.csvFile.text;

		// 行数をIDとしてファイルを作成
		string fileName = $"{csvImporter.csvFile.name}.asset";
		string path = "Assets/Resources/Mapdata/" + fileName;

		// Dataのインスタンスをメモリ上に作成
		var floorData = CreateInstance<MapData>();

		floorData.mapArray2D.Create(20, 20);

		// 改行ごとにパース
		string[] afterParse = csvText.Split('\n');

		for (int i = 0; i < afterParse.Length; i++)
		{
			string[] parseByComma = afterParse[i].Split(',');

            for (int j = 0; j < parseByComma.Length; j++)
            {
                if (int.TryParse(parseByComma[j], out int result))
                {
					floorData.mapArray2D.Set(j, i, result);
					//Debug.Log(mapData.mapArray[i,j]);

                    if (result == 4)
                    {
						floorData.StartPositionList.Add(new Vector3(j, 0, i));
                    }
                    else if (result == 5)
                    {
						floorData.GoalPositionList.Add(new Vector3(j, 0, i));
					}
				}
            }
		}

		// インスタンス化したものをアセットとして保存
		var asset = (MapData)AssetDatabase.LoadAssetAtPath(path, typeof(MapData));
		if (asset == null)
		{
			// 指定のパスにファイルが存在しない場合は新規作成
			AssetDatabase.CreateAsset(floorData, path);
		}
		else
		{
			// 指定のパスに既に同名のファイルが存在する場合は更新
			EditorUtility.CopySerialized(floorData, asset);
			AssetDatabase.SaveAssets();
		}
		AssetDatabase.Refresh();
		Debug.Log(csvImporter.name + " : マップデータの作成が完了しました。");
	}
}
