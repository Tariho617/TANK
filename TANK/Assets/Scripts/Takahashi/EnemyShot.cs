// ---------------------------------------------------------  
// EnemyShot.cs  
//   
// 作成日:  2024.6.17
// 作成者:  髙橋光栄
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("エネミーのスクリプタブルオブジェクト格納用")]
    private EnemyData _enemyData = default;

    [SerializeField, Tooltip("エネミーのオブジェクトプールクラス")]
    private EnemyObjectPool _enemyObjectPool = default;

    private float _enemyShotTime  = default;
    private float _enemyShotSpeed = default;

    #endregion
  
    #region プロパティ  
  
    #endregion
    
    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    private void Awake()
    {
        _enemyShotSpeed = _enemyData.EnemyShotSpped;
        _enemyShotTime  = _enemyData.EnemyShotDisplayTime;
    }

    /// <summary>
    /// 設定したフレーム毎に実行
    /// </summary>
    private void FixedUpdate()
    {
        _enemyShotTime -= Time.deltaTime;

        // エネミーの中心を起点にまっすぐ飛ぶ
        transform.position += transform.forward * _enemyShotSpeed * Time.deltaTime;

        // 
        if (_enemyShotTime <= 0)
        {
            _enemyShotTime = _enemyData.EnemyShotDisplayTime;
            HideFromStage();
        }
    }

    #region privateメソッド群  

    #endregion

    #region publicメソッド群

    /// <summary>
    /// positionを渡された座標に設定
    /// </summary>
    /// <param name="pos"></param>
    public void ShowInStage(Vector3 pos, Quaternion rote)
    {
        transform.position = pos;
        transform.rotation = rote;
    }

    /// <summary>
    /// オブジェクトプールのCollect関数を呼び出し自身を回収
    /// </summary>
    public void HideFromStage()
    {
        _enemyObjectPool.Collect(this);
    }

    #endregion
}
