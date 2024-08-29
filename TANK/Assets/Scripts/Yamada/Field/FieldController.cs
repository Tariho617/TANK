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

    // ���݂̃t�B�[���h�z��
    private int[,] _currentField = default;

    // CSV�t�@�C���̃p�X
    private string _csvFilePath = "Assets/Resources/FieldData/FieldData2.csv";

    #region �v���p�e�B
    public int[,] CurrentField { get => _currentField; set => _currentField = value; }
    #endregion

    // �t�B�[���h�̃I�u�W�F�N�g
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
        // CSV�t�@�C����ǂݍ���
        string[] lines = File.ReadAllLines(_csvFilePath);

        // �s���Ɨ񐔂��擾
        int rowCount = lines.Length;
        int colCount = lines[0].Split(',').Length;

        // _currentField��������
        CurrentField = new int[rowCount, colCount];

        // CSV�t�@�C���̓��e���p�[�X���ē񎟌��z��Ɋi�[
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
    /// �ǐ���
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
    /// �s�T��
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
    /// ��T��
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
    /// �����ǐ���
    /// </summary>
    /// <param name="startY">Y���J�n�ʒu</param>
    /// <param name="startX">X���J�n�ʒu</param>
    /// <param name="length">����</param>
    /// <param name="isHorizontal">�T�������t���O</param>
    private void GenerateMargeWall(int startY, int startX, int length, bool isHorizontal)
    {
        GameObject generateObject = Instantiate(_margeWallPrefab, this.transform);
        Transform objectTransform = generateObject.transform;

        // ���������Ă����ꍇ
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
