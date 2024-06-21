// ---------------------------------------------------------  
// PlayerShot.cs  
//   
// 作成日:  6/12
// 作成者:  山田智哉
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;
using System;

public class PlayerShot : CharacterShot
{

    #region 変数  

    // カメラ
    private Camera _mainCamera = default;

    #endregion

    #region プロパティ  

    #endregion
    
    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    private void Awake()
    {
        // キャッシュ
        _mainCamera = Camera.main;

        // 狙う方向の更新を購読
        _targetDirection.Subscribe(_ =>
        {
            // 角度を取得
            float angle = GetAim(Vector3.zero, _targetDirection.Value);

            // 砲台の向きを調整
            _battery.eulerAngles = new Vector3(0, -angle + 90f, 0);
        });
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        // カメラからのマウス位置を取得
        Vector3 mousePosition = _mainCamera.WorldToScreenPoint(transform.position);

        // 狙う方向を取得
        _targetDirection.Value = Input.mousePosition - mousePosition;
        
    }

    #region privateメソッド群 
    
    /// <summary>
    /// エイム
    /// </summary>
    /// <param name="origin">原点</param>
    /// <param name="Direction">発射方向</param>
    /// <returns>発射角度</returns>
    private float GetAim(Vector2 origin, Vector2 Direction)
    {
        // 方向ベクトルを算出
        float vectorX = Direction.x - origin.x;
        float vectorY = Direction.y - origin.y;

        // ベクトルを角度に変換
        float radian = Mathf.Atan2(vectorY, vectorX);

        // 度数を算出
        float degree = radian * Mathf.Rad2Deg;

        // 度数を返す
        return degree;
    }

    #endregion

    #region publicメソッド群

    public override void Shooting(CharacterData.BulletType shotBulletType , int maxRapidFire, float coolTime)
    {
        // 発射上限以上はリターン
        if (_rapidFireCount >= maxRapidFire)
        {
            return;
        }

        // 一発目の射撃から連射クールタイム開始
        if (_rapidFireCount == 0)
        {
            // 非同期処理
            Observable.Timer(TimeSpan.FromSeconds(coolTime))
                // クールタイム終了で連射カウントを初期化
                .Subscribe(_ => _rapidFireCount = 0)
                .AddTo(this);
        }

        // 発射方向
        Vector3 shotDirection = _targetDirection.Value - transform.position;

        // 弾を発射
        ObjectPoolController.instance.Lend
            (transform.TransformPoint(
                _battery.forward * SHOT_POSITION_DISTANCE + new Vector3(0, transform.localScale.z * SHOT_POSITION_HEIGHT, 0)),
                Quaternion.LookRotation(new Vector3(shotDirection.x, 0, shotDirection.y)),
                (int)shotBulletType
            );

        // 連射カウントを加算
        _rapidFireCount++;

    }

    #endregion
}
