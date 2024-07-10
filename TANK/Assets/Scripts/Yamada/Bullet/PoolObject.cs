using System;
using UniRx;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField, Tooltip("�e�X�e�[�^�X")]
    protected BulletStatus _bulletStatus = default;
    protected BulletData _bulletData = default;

    // ������Transform
    protected Transform _thisTransform = default;

    // �e�̎��
    protected int _poolObjectNumber = default;

    // �������ԃ^�C�}�[�j���p
    protected IDisposable _lifeTimeTimer = default;

    // �j���C�x���g
    protected Subject<Unit> _destroyEvent = new();

    #region �v���p�e�B

    public IObservable<Unit> OnDestroy => _destroyEvent;

    #endregion

    /// <summary>
    /// �N��������
    /// </summary>
    protected void Awake()
    {
        // �L���b�V����������
        _thisTransform = this.transform;
        _bulletData = _bulletStatus._bulletData;
        this.gameObject.SetActive(false);
    }

    #region public���\�b�h�Q

    /// <summary>
    /// �o����
    /// </summary>
    public virtual void AppearanceObject(int poolObjectNumber)
    {
        // �e���ݒ�
        _poolObjectNumber = poolObjectNumber;

        // �������Ԃ��߂���Ƌ����I�ɕԋp
        _lifeTimeTimer = Observable.Timer(TimeSpan.FromSeconds(_bulletData._lifeTime))
            .Subscribe(_ =>
            {
                HideObject();
            })
            .AddTo(this);
    }

    #endregion

    #region private���\�b�h�Q

    /// <summary>
    /// �ԋp��
    /// </summary>
    protected virtual void HideObject()
    {
        _destroyEvent.OnNext(Unit.Default);
        _lifeTimeTimer?.Dispose();
        ObjectPoolController.instance.Collect(this, _poolObjectNumber);
    }

    #endregion
}