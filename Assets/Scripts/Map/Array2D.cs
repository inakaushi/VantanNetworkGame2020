using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Array2D
{
	[SerializeField]
	int _width; // 幅

	[SerializeField]
	int _height; // 高さ

	[SerializeField]
	int _outOfRange = -1; // 領域外を指定した時の値

	[SerializeField]
	int[] _values = null; // マップデータ
	/// 幅
	public int Width
	{
		get { return _width; }
	}
	/// 高さ
	public int Height
	{
		get { return _height; }
	}

	/// 作成
	public void Create(int width, int height)
	{
		_width = width;
		_height = height;
		_values = new int[Width * Height];
	}

	/// 座標をインデックスに変換する
	public int ToIdx(int x, int z)
	{
		return x + (z * Width);
	}

	/// 領域外かどうかチェックする
	public bool IsOutOfRange(int x, int z)
	{
		if (x < 0 || z >= Width) { return true; }
		if (x < 0 || z >= Height) { return true; }

		// 領域内
		return false;
	}
	/// 値の取得
	// @param x X座標
	// @param y Y座標
	// @return 指定の座標の値（領域外を指定したら_outOfRangeを返す）
	public int Get(int x, int z)
	{
		if (IsOutOfRange(x, z))
		{
			return _outOfRange;
		}

		return _values[z * Width + x];
	}

	/// 値の設定
	// @param x X座標
	// @param y Y座標
	// @param v 設定する値
	public void Set(int x, int z, int v)
	{
		if (IsOutOfRange(x, z))
		{
			// 領域外を指定した
			return;
		}

		_values[z * Width + x] = v;
	}

	/// デバッグ出力
	public void Dump()
	{
		Debug.Log("[Array2D] (w,h)=(" + Width + "," + Height + ")");
		for (int y = 0; y < Height; y++)
		{
			string s = "";
			for (int x = 0; x < Width; x++)
			{
				s += Get(x, y) + ",";
			}
			Debug.Log(s);
		}
	}
}
