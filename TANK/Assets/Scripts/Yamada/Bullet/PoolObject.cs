using System;
using UniRx;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField, Tooltip("弾ステータス")]
    protected BulletStatus _bulletStatus = default;
    protected BulletData _bulletData = default;

    // 自分のTransform
    protected Transform _thisTransform = default;

    // 弾の種類
    protected int _poolObjectNumber = default;

    // 生存時間タイマー破棄用
    protected IDisposable _lifeTimeTimer = default;

    // 破棄イベント
    protected Subject<Unit> _destroyEvent = new();

    #region プロパティ

    public IObservable<Unit> OnDestroy => _destroyEvent;

    #endregion

    /// <summary>
    /// 起動時処理
    /// </summary>
    protected void Awake()
    {
        // キャッシュ＆初期化
        _thisTransform = this.transform;
        _bulletData = _bulletStatus._bulletData;
        this.gameObject.SetActive(false);
    }

    #region publicメソッド群

    /// <summary>
    /// 出現時
    /// </summary>
    public virtual void AppearanceObject(int poolObjectNumber)
    {
        // 弾種を設定
        _poolObjectNumber = poolObjectNumber;

        // 生存時間を過ぎると強制的に返却
        _lifeTimeTimer = Observable.Timer(TimeSpan.FromSeconds(_bulletData._lifeTime))
            .Subscribe(_ =>
            {
                HideObject();
            })
            .AddTo(this);
    }

    #endregion

    #region privateメソッド群

    /// <summary>
    /// 返却時
    /// </summary>
    protected virtual void HideObject()
    {
        _destroyEvent.OnNext(Unit.Default);
        _lifeTimeTimer?.Dispose();
        ObjectPoolController.instance.Collect(this, _poolObjectNumber);
    }

    #endregion
}