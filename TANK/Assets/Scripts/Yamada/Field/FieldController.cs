using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    [SerializeField]
    private GameObject _wallPrefab = default;

    [SerializeField]
    private GameObject _margeWallPrefab = default;

    // 現在のフィールド配列
    private int[,] _currentField = default;

    // CSVファイルのパス
    private string _csvFilePath = "Assets/Resources/FieldData/FieldData2.csv";

    #region プロパティ
    public int[,] CurrentField { get => _currentField; set => _currentField = value; }
    #endregion

    // フィールドのオブジェクト
    private enum FieldObject
    {
        None,
        Wall,
        Fragile,
        KeepOut
    }

    void Start()
    {
        LoadCSVFile();
        
    }

    private void LoadCSVFile()
    {
        // CSVファイルを読み込む
        string[] lines = File.ReadAllLines(_csvFilePath);

        // 行数と列数を取得
        int rowCount = lines.Length;
        int colCount = lines[0].Split(',').Length;

        // _currentFieldを初期化
        CurrentField = new int[rowCount, colCount];

        // CSVファイルの内容をパースして二次元配列に格納
        for (int y = 0; y < rowCount; y++)
        {
            string[] lineData = lines[y].Split(',');
            for (int x = 0; x < colCount; x++)
            {
                CurrentField[y, x] = int.Parse(lineData[x]);
            }
        }
        GenerateWalls();
    }

    /// <summary>
    /// 壁生成
    /// </summary>
    private void GenerateWalls()
    {
        GameObject generateObject;
        for (int y = 0; y < CurrentField.GetLength(0); y++)
        {
            for (int x = 0; x < CurrentField.GetLength(1); x++)
            {
                if (CurrentField[y, x] == (int)FieldObject.Wall)
                {
                    generateObject = Instantiate(_wallPrefab, this.transform);
                    generateObject.transform.position = new Vector3(x, 0, -y);
                }
            }
        }
        SearchRow();
        SearchColumn();
    }


    /// <summary>
    /// 行探索
    /// </summary>
    private void SearchRow()
    {
        for (int y = 0; y < CurrentField.GetLength(0); y++)
        {
            int wallCount = 0;
            for (int x = 0; x < CurrentField.GetLength(1); x++)
            {
                if (CurrentField[y, x] == (int)FieldObject.Wall)
                {
                    wallCount++;
                }
                else if (wallCount > 1)
                {
                    GenerateMargeWall(y, x - wallCount, wallCount, true);
                    wallCount = 0;
                }
                else
                {
                    wallCount = 0;
                }
            }

            if (wallCount > 1)
            {
                GenerateMargeWall(y, CurrentField.GetLength(1) - wallCount, wallCount, true);
            }
        }
    }

    /// <summary>
    /// 列探索
    /// </summary>
    private void SearchColumn()
    {
        for (int x = 0; x < CurrentField.GetLength(1); x++)
        {
            int wallCount = 0;
            for (int y = 0; y < CurrentField.GetLength(0); y++)
            {
                if (CurrentField[y, x] == (int)FieldObject.Wall)
                {
                    wallCount++;
                }
                else if (wallCount > 1)
                {
                    GenerateMargeWall(y - wallCount, x, wallCount, false);
                    wallCount = 0;
                }
                else
                {
                    wallCount = 0;
                }
            }

            if (wallCount > 1)
            {
                GenerateMargeWall(CurrentField.GetLength(0) - wallCount, x, wallCount, false);
            }
        }
    }

    /// <summary>
    /// 統合壁生成
    /// </summary>
    /// <param name="startY">Y軸開始位置</param>
    /// <param name="startX">X軸開始位置</param>
    /// <param name="length">長さ</param>
    /// <param name="isHorizontal">探索方向フラグ</param>
    private void GenerateMargeWall(int startY, int startX, int length, bool isHorizontal)
    {
        GameObject generateObject = Instantiate(_margeWallPrefab, this.transform);
        Transform objectTransform = generateObject.transform;

        // 横が続いていた場合
        if (isHorizontal)
        {
            objectTransform.position = new Vector3(
                startX + length / 2f - 0.5f, 
                0,
                -startY);

            objectTransform.localScale =  new Vector3(
                 length, 
                 objectTransform.localScale.y,
                 objectTransform.localScale.z);
        }
        else
        {
            objectTransform.position = new Vector3(
                startX,
                0,
                -startY - length / 2f + 0.5f);

            objectTransform.localScale = new Vector3(
                 objectTransform.localScale.x,
                 objectTransform.localScale.y,
                 length);
        }
    }

}
