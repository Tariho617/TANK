// ---------------------------------------------------------  
// EnemyObjectPool.cs  
//   
// 作成日:  2024.6.17
// 作成者:  髙橋光栄
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections.Generic;

public class EnemyObjectPool : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("エネミーのスクリプタブルオブジェクト格納用")]
    private EnemyData _enemyData = default;

    [SerializeField, Tooltip("エネミーショットクラス")]
    private EnemyShot _enemyShot = default;

    Queue<EnemyShot> EnemyQueue;

    #endregion
  
    #region プロパティ  
  
    #endregion
    
     /// <summary>  
     /// 初期化処理  
     /// </summary>  
     private void Awake()
     {

        EnemyQueue = new Queue<EnemyShot>();

        for (int i = 0; i < _enemyData.EnemyMaxPoolCount; i++)
        {

            // 生成する
            EnemyShot tmpEnemyShot = Instantiate(_enemyShot, transform);

            // Queueに追加
            EnemyQueue.Enqueue(tmpEnemyShot);

        }
     }

    #region privateメソッド群  

    #endregion

    #region publicメソッド群

    /// <summary>
    /// 弾を貸し出す処理
    /// </summary>
    /// <param name="pos">プレイヤーの位置</param>
    public EnemyShot EnemyShotLaunch(Vector3 pos, Quaternion rote)
    {

        //Queueが空なら生成する
        if (EnemyQueue.Count <= 0)
            return Instantiate(_enemyShot);

        //Queueから弾を一つ取り出す
        EnemyShot tmpShot = EnemyQueue.Dequeue();

        //弾を表示する
        tmpShot.gameObject.SetActive(true);

        //渡された座標に弾を移動する
        tmpShot.ShowInStage(pos, rote);

        //呼び出し元に渡す
        return tmpShot;
    }

    /// <summary>
    /// 弾の回収処理
    /// </summary>
    /// <param name="_shot"></param>
    public void Collect(EnemyShot _enemyShot)
    {

        //弾のゲームオブジェクトを非表示
        _enemyShot.gameObject.SetActive(false);

        //Queueに格納
        EnemyQueue.Enqueue(_enemyShot);
    }

    #endregion
}
