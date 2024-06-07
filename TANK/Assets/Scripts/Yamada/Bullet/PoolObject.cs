using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    // �I�u�W�F�N�g�v�[��
    protected ObjectPoolController _objectPool = default;

    // ������Transform
    protected Transform _thisTransform = default;

    // ���
    protected int _objectTypeNumber = default;

    protected void Awake()
    {
        // �L���b�V����������
        _thisTransform = this.transform;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// �o�����\�b�h
    /// </summary>
    public abstract void AppearanceObject();

    /// <summary>
    /// �ԋp���\�b�h
    /// </summary>
    protected abstract void HideObject();
}