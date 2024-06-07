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

    // 反射数のカウント変数
    private int _reflectionCount = default;

    // rigidbody
    private Rigidbody _rigidbody = default;

    // 移動方向
    private Vector3 _moveDirection = default;

    // 生存時間タイマー
    private IDisposable _lifeTimeTimer = default;

    // 生存時間
    [SerializeField]
    private float _lifeTime = 10f;

    private Subject<Unit> _lifeTimeSubject = new();
    public IObservable<Unit> OnLifeTime => _lifeTimeSubject;

    private bool _ignoreCollisions = default;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        // 衝突処理
        this.OnCollisionEnterAsObservable()
            .Where(_ => !_ignoreCollisions)
            .Subscribe(collision => 
            {
                // ダメージを与えられる場合
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    // ダメージを与え、弾を返却し、処理終了
                    damageable.ReceiveDamage();
                    HideObject();
                    return;
                }

                // 反射数が限界値以上の場合
                if (_reflectionCount >= _reflectionLimit)
                {
                    
                    // 弾を返却し、処理終了
                    HideObject();
                    return;
                }

                _ignoreCollisions = true;

                // 一定時間後にフラグをリセット
                Observable.TimerFrame(1)
                    .Subscribe(_ => _ignoreCollisions = false)
                    .AddTo(this);

                // 反射数を加算
                _reflectionCount++;

                // 衝突点の法線ベクトルを取得
                Vector3 normal = collision.contacts[0].normal;

                // 入射ベクトルを取得
                Vector3 incomingVector = _moveDirection.normalized;

                // 反射ベクトルを計算
                Vector3 reflectVector = incomingVector - 2f * Vector3.Dot(incomingVector, normal) * normal;

                // 反射ベクトルを移動方向に設定
                _moveDirection = reflectVector.normalized;

            });

        // 移動方向に速度をかけて進む
        this.FixedUpdateAsObservable()
            .Where(_ => _speed > 0f)
            .Subscribe(_ =>
            {
                _rigidbody.velocity = _moveDirection * _speed;
            });

        // 非アクティブ時処理
        this.OnDisableAsObservable()
            .Subscribe(_ =>
            {
                // 初期化
                _reflectionCount = 0;
                _lifeTimeTimer?.Dispose();

            })
            .AddTo(this);
            
    }


    public override void AppearanceObject()
    {
        // 移動方向を正面に設定
        _moveDirection = transform.forward;
        _ignoreCollisions = false;
        // 生存時間を過ぎると強制的に返却
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
        //            print("時間切れ");
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
