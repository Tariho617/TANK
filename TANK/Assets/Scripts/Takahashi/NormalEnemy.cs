// ---------------------------------------------------------  
// NormalEnemy.cs  
//   
// 作成日:  2024.7.1
// 作成者:  髙橋光栄
// ---------------------------------------------------------  
using UnityEngine;

/// <summary>
/// 標準エネミーを制御するクラス
/// </summary>
public class NormalEnemy : EnemyManager
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
     protected override void Update ()
     {
        base.Update();
        // Ray処理
        PlayerTargetingRay();
     }

    #region privateメソッド群  

    #endregion

    #region publicメソッド群

    public override void Shot()
    {
        base.Shot();


        // プレイヤーの方向に弾を発射
        Vector3 direction = (_playerPos - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        _enemyObjectPool.EnemyShotLaunch(transform.position, rotation);

        // ショットクールタイム制御
        EnemyShotCoolTime();
    }
    #endregion
}
