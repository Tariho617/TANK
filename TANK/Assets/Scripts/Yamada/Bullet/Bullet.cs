
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class Bullet : PoolObject, IDamageable
{
    #region �ϐ�

    // ���ː��̃J�E���g�ϐ�
    private int _reflectionCount = default;

    // rigidbody
    private Rigidbody _rigidbody = default;

    // �ړ�����
    private Vector3 _moveDirection = default;

    // �Փ˔��萧��p�t���O
    private bool _ignoreCollisions = default;

    #endregion


    /// <summary>  
    /// �X�V�O����  
    /// </summary>
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        CollsionDetection();

        Move();

    }

    #region private���\�b�h�Q

    /// <summary>
    /// �ړ�
    /// </summary>
    private void Move()
    {
        // �ړ������ɑ��x�������Đi��
        this.FixedUpdateAsObservable()
            .Where(_ => _bulletData._speed > 0f)
            .Subscribe(_ =>
            {
                _rigidbody.velocity = _moveDirection * _bulletData._speed;
            });
    }

    /// <summary>
    /// �����蔻��
    /// </summary>
    private void CollsionDetection()
    {
        // �Փˏ���
        this.OnCollisionEnterAsObservable()
            // �Փ˖�����Ԃ̂Ƃ��͏������Ȃ�
            .Where(_ => !_ignoreCollisions)
            .Subscribe(collision =>
            {
                // �_���[�W��^������ꍇ
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {

                    // �_���[�W��^���A�e��ԋp���A�����I��
                    damageable.ReceiveDamage();
                    HideObject();
                    return;
                }

                // ���ː������E�l�ȏ�̏ꍇ
                if (_reflectionCount >= _bulletData._reflections)
                {

                    // �e��ԋp���A�����I��
                    HideObject();
                    return;
                }

                // ���i����j�~---------------------------------
                // �Փ˖�����Ԃɂ���
                _ignoreCollisions = true;

                // �Փ˖���������
                Observable.TimerFrame(1)
                    .Subscribe(_ => _ignoreCollisions = false)
                    .AddTo(this);
                // ---------------------------------------------

                // ���ː������Z
                _reflectionCount++;

                // �Փ˓_�̖@���x�N�g�����擾
                Vector3 normal = collision.contacts[0].normal;

                // ���˃x�N�g�����擾
                Vector3 incomingVector = _moveDirection.normalized;

                // ���˃x�N�g�����v�Z
                Vector3 reflectVector = incomingVector - 2f * Vector3.Dot(incomingVector, normal) * normal;

                // ���˃x�N�g�����ړ������ɐݒ�
                _moveDirection = reflectVector.normalized;

            });

        this.OnTriggerEnterAsObservable()
            .Subscribe(collision =>
            {
                // �_���[�W��^������ꍇ
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    // �_���[�W��^���A�e��ԋp���A�����I��
                    damageable.ReceiveDamage();
                    HideObject();
                    return;
                }
            });
    }

    protected override void HideObject()
    {
        base.HideObject();
        _reflectionCount = 0;
    }

    #endregion

    #region public���\�b�h�Q

    public override void AppearanceObject(int poolObjectNumber)
    {
        base.AppearanceObject(poolObjectNumber);
        // �ړ������𐳖ʂɐݒ�
        _moveDirection = transform.forward;
        // �Փ˔��萧��p�t���O���U��
        _ignoreCollisions = false;

        _reflectionCount = 0;
    }

    public void ReceiveDamage()
    {
        print("���E");
    }

    #endregion

}
