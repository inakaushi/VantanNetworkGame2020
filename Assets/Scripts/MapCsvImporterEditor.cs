using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapCsvImporter))]
public class MapCsvImporterEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("マップデータの作成"))
		{
			Debug.Log("マップデータの作成ボタンが押された");
		}
	}
}
