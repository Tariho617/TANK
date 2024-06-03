// ---------------------------------------------------------  
// StationaryEnemy.cs  
//   
// 作成日:  2024/6/3
// 作成者:  髙橋光栄
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

/// <summary>
/// 固定砲台のエネミーを制御するクラス
/// </summary>
public class StationaryEnemy : EnemyManager
{
  
    #region 変数  
  
    #endregion
  
    #region プロパティ  
  
    #endregion
  
    #region メソッド  
  
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
     private void Update ()
     {
        PlayerTargetingRay();
     }

    public override void Shot()
    {
        base.Shot();
        print("撃った");
    }

    #endregion
}
