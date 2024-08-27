// ---------------------------------------------------------  
// StationaryEnemy.cs  
//   
// 作成日:  2024/6/3
// 作成者:  髙橋光栄
// ---------------------------------------------------------  
using UnityEngine;

/// <summary>
/// 固定砲台のエネミーを制御するクラス
/// </summary>
public class StationaryEnemy : EnemyManager
{

    #region 変数  

    [SerializeField, Tooltip("エネミーのオブジェクトプールクラス")]
    private EnemyObjectPool _enemyObjectPool = default;

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
     private new void Update ()
     {
        PlayerTargetingRay();
     }

    #region privateメソッド群  

    #endregion

    #region publicメソッド群

    public override void Shot()
    {
        base.Shot();
    }

    #endregion
}
