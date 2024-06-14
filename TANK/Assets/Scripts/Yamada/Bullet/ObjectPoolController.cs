// ---------------------------------------------------------
// ObjectPoolController.cs
//
// 作成日:2月5日
// 作成者:山田智哉
// ---------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトプールクラス
/// </summary>
public class ObjectPoolController : MonoBehaviour
{
    #region 変数

    public static ObjectPoolController instance;

    // オブジェクトの生成数
    [Header("生成数"), SerializeField]
    private int _createCount = 30;

    // オブジェクトの配列
    [Header("プール化するオブジェクト")]
    public PoolObject[] _objectType = default;

    // _objectTypeのプロパティ
    public PoolObject[] ObjectType { get => _objectType; set => _objectType = value; }

    // オブジェクトのQueueを入れるlist
    private List<Queue<PoolObject>> _poolList = new();

    #endregion

    

    /// <summary>
    /// 起動時処理
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // 作成したオブジェクトの要素数分、繰り返す
        for (int i = 0; i < ObjectType.Length; i++)
        {
            //Queueを作り_poolListに追加
            _poolList.Add(new Queue<PoolObject>(i));

            // _maxCount分の弾を生成
            for (int j = 0; j < _createCount; j++)
            {
                // 生成
                PoolObject generateObject = Instantiate(ObjectType[i], transform.position, transform.rotation, transform);

                // 非アクティブ化
                generateObject.gameObject.SetActive(false);

                // 指定したlist内のQueueに生成したオブジェクトを追加
                _poolList[i].Enqueue(generateObject);
            }
        }
    }

    #region publicメソッド群

    /// <summary>
    /// オブジェクト貸し出しメソッド
    /// </summary>
    /// <param name="spawnPosition">出現位置</param>
    /// <param name="angle">出現時の角度</param>
    /// <returns>借りるオブジェクト</returns>
    public PoolObject Lend(Vector3 spawnPosition, Quaternion angle, int poolObjectNumber)
    {
        // Queueが空ならnullを渡す
        if (_poolList[poolObjectNumber].Count <= 0)
        {
            return null;
        }

        // Queueから指定したオブジェクトを一つ取り出す
        PoolObject lendObject = _poolList[poolObjectNumber].Dequeue();

        // 渡された座標に移動
        lendObject.transform.position = spawnPosition;

        // 渡された角度に調整
        lendObject.transform.rotation = angle;

        // 出現メソッドを呼び出す
        lendObject.AppearanceObject(poolObjectNumber);

        // オブジェクトを表示する
        lendObject.gameObject.SetActive(true);

        // 呼び出し元に渡す
        return lendObject;
    }

    /// <summary>
    /// オブジェクト回収メソッド
    /// </summary>
    /// <param name="collectObject">回収するオブジェクト</param>
    /// <param name="poolObjectNumber">回収するオブジェクトの判別番号</param>
    public void Collect(PoolObject collectObject, int poolObjectNumber)
    {
        // オブジェクトを非表示
        collectObject.gameObject.SetActive(false);

        // 回収したオブジェクトをQueueに再度追加
        _poolList[poolObjectNumber].Enqueue(collectObject);
    }

    #endregion
}