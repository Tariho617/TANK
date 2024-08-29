
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class Bullet : PoolObject, IDamageable
{
    #region 変数

    // 反射数のカウント変数
    private int _reflectionCount = default;

    // rigidbody
    private Rigidbody _rigidbody = default;

    // 移動方向
    private Vector3 _moveDirection = default;

    // 衝突判定制御用フラグ
    private bool _ignoreCollisions = default;

    #endregion


    /// <summary>  
    /// 更新前処理  
    /// </summary>
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        CollsionDetection();

        Move();

    }

    #region privateメソッド群

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        // 移動方向に速度をかけて進む
        this.FixedUpdateAsObservable()
            .Where(_ => _bulletData._speed > 0f)
            .Subscribe(_ =>
            {
                _rigidbody.velocity = _moveDirection * _bulletData._speed;
            });
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    private void CollsionDetection()
    {
        // 衝突処理
        this.OnCollisionEnterAsObservable()
            // 衝突無視状態のときは処理しない
            .Where(_ => !_ignoreCollisions)
            .Subscribe(collision =>
            {
                // ダメージを与えられる場合
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {

                    // ダメージを与え、弾を返却し、処理終了
                    damageable.ReceiveDamage();
                    HideObject();
                    return;
                }

                // 反射数が限界値以上の場合
                if (_reflectionCount >= _bulletData._reflections)
                {

                    // 弾を返却し、処理終了
                    HideObject();
                    return;
                }

                // 多段判定阻止---------------------------------
                // 衝突無視状態にする
                _ignoreCollisions = true;

                // 衝突無視を解除
                Observable.TimerFrame(1)
                    .Subscribe(_ => _ignoreCollisions = false)
                    .AddTo(this);
                // ---------------------------------------------

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

        this.OnTriggerEnterAsObservable()
            .Subscribe(collision =>
            {
                // ダメージを与えられる場合
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    // ダメージを与え、弾を返却し、処理終了
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

    #region publicメソッド群

    public override void AppearanceObject(int poolObjectNumber)
    {
        base.AppearanceObject(poolObjectNumber);
        // 移動方向を正面に設定
        _moveDirection = transform.forward;
        // 衝突判定制御用フラグを偽に
        _ignoreCollisions = false;

        _reflectionCount = 0;
    }

    public void ReceiveDamage()
    {
        print("相殺");
    }

    #endregion

}
