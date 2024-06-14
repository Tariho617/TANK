// ---------------------------------------------------------  
// EnemyManager.cs  
//   
// 作成日:  2024/5/31
// 作成者:  髙橋光栄
// ---------------------------------------------------------  
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class EnemyManager : CharacterManager,IShootable
{
    #region 取得関連

    [SerializeField, Tooltip("エネミーのスクリプタブルオブジェクト格納用")]
    private EnemyData _enemyData = default;


    #endregion

    #region 変数

    [SerializeField,Tooltip("常に追尾するターゲット")]
    private GameObject _player = default;

    [SerializeField, Tooltip("Rayの長さ")]
    private float _rayLength = default;

    // 現在、エネミーは撃てるか(true = 撃てる)
    private bool _isFire = true;

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
    private void Start()
    {

    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {

    }

    #region privateメソッド群  

    #endregion

    #region publicメソッド群

    /// <summary>
    /// エネミーのRayを制御するメソッド
    /// </summary>
    public void PlayerTargetingRay()
    {
        if (_player == null)
        {
            Debug.LogWarning("Player Transform is not assigned.");
            return;
        }

        Vector3 direction = (_player.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        // レイを可視化
        Debug.DrawRay(transform.position, direction * _rayLength, Color.red);

        if (Physics.Raycast(ray, out hit, _rayLength))
        {

            // RayがプレイヤーにHitしていて尚且つ、撃てる状態か
            if (hit.collider.gameObject.CompareTag("Player")&&_isFire)
            {
                Debug.Log("プレイヤーにRayがHit!");
                _isFire = false;
                Shot();
            }
            else
            {
                Debug.Log("障害物にRayがHit!");
            }
        }
    }

    /// <summary>
    /// ショットクールタイム制御
    /// </summary>
    protected async void EnemyShotCoolTime()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_enemyData.EnemyShotCoolTime));
        _isFire = true;
    }

    /// <summary>
    /// 継承用空メソッド
    /// </summary>
    public virtual void Shot() { }

    #endregion
}
