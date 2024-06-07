using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class Bullet : PoolObject, IDamageable
{
    [SerializeField]
    private float _speed = default;

    [SerializeField]
    private int _reflectionLimit = default;

    // ���ː��̃J�E���g�ϐ�
    private int _reflectionCount = default;

    // rigidbody
    private Rigidbody _rigidbody = default;

    // �ړ�����
    private Vector3 _moveDirection = default;

    // �������ԃ^�C�}�[
    private IDisposable _lifeTimeTimer = default;

    // ��������
    [SerializeField]
    private float _lifeTime = 10f;

    private Subject<Unit> _lifeTimeSubject = new();
    public IObservable<Unit> OnLifeTime => _lifeTimeSubject;

    private bool _ignoreCollisions = default;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        // �Փˏ���
        this.OnCollisionEnterAsObservable()
            .Where(_ => !_ignoreCollisions)
            .Subscribe(collision => 
            {
                // �_���[�W��^������ꍇ
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    // �_���[�W��^���A�e��ԋp���A�����I��
                    damageable.ReceiveDamage();
                    HideObject();
                    return;
                }

                // ���ː������E�l�ȏ�̏ꍇ
                if (_reflectionCount >= _reflectionLimit)
                {
                    
                    // �e��ԋp���A�����I��
                    HideObject();
                    return;
                }

                _ignoreCollisions = true;

                // ��莞�Ԍ�Ƀt���O�����Z�b�g
                Observable.TimerFrame(1)
                    .Subscribe(_ => _ignoreCollisions = false)
                    .AddTo(this);

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

        // �ړ������ɑ��x�������Đi��
        this.FixedUpdateAsObservable()
            .Where(_ => _speed > 0f)
            .Subscribe(_ =>
            {
                _rigidbody.velocity = _moveDirection * _speed;
            });

        // ��A�N�e�B�u������
        this.OnDisableAsObservable()
            .Subscribe(_ =>
            {
                // ������
                _reflectionCount = 0;
                _lifeTimeTimer?.Dispose();

            })
            .AddTo(this);
            
    }


    public override void AppearanceObject()
    {
        // �ړ������𐳖ʂɐݒ�
        _moveDirection = transform.forward;
        _ignoreCollisions = false;
        // �������Ԃ��߂���Ƌ����I�ɕԋp
       _lifeTimeTimer = Observable.Timer(TimeSpan.FromSeconds(_lifeTime))
           .Subscribe(_ =>
           {

               HideObject();

           })
           .AddTo(this);

        //_lifeTimeTimer = Observable.Timer(TimeSpan.FromSeconds(_lifeTime / 2), TimeSpan.FromSeconds(_lifeTime / 10))
        //    .Subscribe(time =>
        //    {
        //        _lifeTimeSubject.OnNext(Unit.Default);
        //        if (time + (_lifeTime / 2) >= _lifeTime)
        //        {
        //            print("���Ԑ؂�");
        //            HideObject();
        //        }
        //    })
        //    .AddTo(this);
    }

    protected override void HideObject()
    {
        ObjectPoolController.instance.Collect(this, _objectTypeNumber);
    }

    public void ReceiveDamage()
    {
        
    }
}
