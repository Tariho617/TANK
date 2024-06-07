// ---------------------------------------------------------  
// PlayerShot.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System;
using UniRx;

public class PlayerShot : CharacterShot
{
  
    #region 変数  
  
    #endregion
  
    #region プロパティ  
  
    #endregion
    
    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    private void Awake()
    {
        
    }
  
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {

    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        Vector3 mousePosition = Camera.main.WorldToScreenPoint(transform.position);
        _targetDirection = Input.mousePosition - mousePosition;
        float angle = GetAim(Vector3.zero, _targetDirection);

        _battery.transform.eulerAngles = new Vector3(0, -angle + 90f, 0);

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

    public override void Shooting()
    {
        // 発射上限以上はリターン
        if (_rapidFireCount >= RAPID_FIRE_LIMIT)
        {
            return;
        }

        // 一発目の射撃から連射クールタイム開始
        if (_rapidFireCount == 0)
        {
            // 非同期処理
            Observable.Timer(TimeSpan.FromSeconds(RAPID_FIRE_COOLTIME))
                .Subscribe(_ => _rapidFireCount = 0)
                .AddTo(this);
        }

        Vector3 shotDirection = _targetDirection - transform.position;

        ObjectPoolController.instance.Lend
            (transform.TransformPoint(
                _battery.transform.forward * SHOT_POSITION_DISTANCE + new Vector3(0, transform.localScale.z, 0)),
                Quaternion.LookRotation(new Vector3(shotDirection.x, 0, shotDirection.y))
            );
        _rapidFireCount++;

    }

    #endregion
}
