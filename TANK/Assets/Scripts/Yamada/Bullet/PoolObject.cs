using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    // オブジェクトプール
    protected ObjectPoolController _objectPool = default;

    // 自分のTransform
    protected Transform _thisTransform = default;

    // 種類
    protected int _objectTypeNumber = default;

    protected void Awake()
    {
        // キャッシュ＆初期化
        _thisTransform = this.transform;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 出現メソッド
    /// </summary>
    public abstract void AppearanceObject();

    /// <summary>
    /// 返却メソッド
    /// </summary>
    protected abstract void HideObject();
}