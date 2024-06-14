// ---------------------------------------------------------
// ObjectPoolController.cs
//
// �쐬��:2��5��
// �쐬��:�R�c�q��
// ---------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�v�[���N���X
/// </summary>
public class ObjectPoolController : MonoBehaviour
{
    #region �ϐ�

    public static ObjectPoolController instance;

    // �I�u�W�F�N�g�̐�����
    [Header("������"), SerializeField]
    private int _createCount = 30;

    // �I�u�W�F�N�g�̔z��
    [Header("�v�[��������I�u�W�F�N�g")]
    public PoolObject[] _objectType = default;

    // _objectType�̃v���p�e�B
    public PoolObject[] ObjectType { get => _objectType; set => _objectType = value; }

    // �I�u�W�F�N�g��Queue������list
    private List<Queue<PoolObject>> _poolList = new();

    #endregion

    

    /// <summary>
    /// �N��������
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // �쐬�����I�u�W�F�N�g�̗v�f�����A�J��Ԃ�
        for (int i = 0; i < ObjectType.Length; i++)
        {
            //Queue�����_poolList�ɒǉ�
            _poolList.Add(new Queue<PoolObject>(i));

            // _maxCount���̒e�𐶐�
            for (int j = 0; j < _createCount; j++)
            {
                // ����
                PoolObject generateObject = Instantiate(ObjectType[i], transform.position, transform.rotation, transform);

                // ��A�N�e�B�u��
                generateObject.gameObject.SetActive(false);

                // �w�肵��list����Queue�ɐ��������I�u�W�F�N�g��ǉ�
                _poolList[i].Enqueue(generateObject);
            }
        }
    }

    #region public���\�b�h�Q

    /// <summary>
    /// �I�u�W�F�N�g�݂��o�����\�b�h
    /// </summary>
    /// <param name="spawnPosition">�o���ʒu</param>
    /// <param name="angle">�o�����̊p�x</param>
    /// <returns>�؂��I�u�W�F�N�g</returns>
    public PoolObject Lend(Vector3 spawnPosition, Quaternion angle, int poolObjectNumber)
    {
        // Queue����Ȃ�null��n��
        if (_poolList[poolObjectNumber].Count <= 0)
        {
            return null;
        }

        // Queue����w�肵���I�u�W�F�N�g������o��
        PoolObject lendObject = _poolList[poolObjectNumber].Dequeue();

        // �n���ꂽ���W�Ɉړ�
        lendObject.transform.position = spawnPosition;

        // �n���ꂽ�p�x�ɒ���
        lendObject.transform.rotation = angle;

        // �o�����\�b�h���Ăяo��
        lendObject.AppearanceObject(poolObjectNumber);

        // �I�u�W�F�N�g��\������
        lendObject.gameObject.SetActive(true);

        // �Ăяo�����ɓn��
        return lendObject;
    }

    /// <summary>
    /// �I�u�W�F�N�g������\�b�h
    /// </summary>
    /// <param name="collectObject">�������I�u�W�F�N�g</param>
    /// <param name="poolObjectNumber">�������I�u�W�F�N�g�̔��ʔԍ�</param>
    public void Collect(PoolObject collectObject, int poolObjectNumber)
    {
        // �I�u�W�F�N�g���\��
        collectObject.gameObject.SetActive(false);

        // ��������I�u�W�F�N�g��Queue�ɍēx�ǉ�
        _poolList[poolObjectNumber].Enqueue(collectObject);
    }

    #endregion
}