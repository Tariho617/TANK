// ---------------------------------------------------------  
// EnemyManager.cs  
//   
// 作成日:  2024/5/31
// 作成者:  髙橋光栄
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;
using Random = UnityEngine.Random; // ランダムで喧嘩しない用

public class EnemyManager : CharacterManager,IShootable
{
    #region 取得関連

    [SerializeField, Tooltip("エネミーのスクリプタブルオブジェクト格納用")]
    private EnemyData _enemyData = default;

    #endregion

    #region 変数

    [SerializeField,Tooltip("常に追尾するターゲット")]
    private GameObject _rayTargetObj = default;

    [SerializeField, Tooltip("Rayの長さ")]
    private float _rayLength = default;

    // 現在、エネミーは撃てるか(true = 撃てる)
    private bool _isFire = true;

    // プレイヤーチェックを一度のみ実行させる(true = サーチ可能)
    private bool _isPlayerSearch = true;

    // RayがHitしたときのプレイヤーの位置
    protected Vector3 _playerPos = default;

    [SerializeField,Tooltip("Rigidbody格納用変数")]
    private Rigidbody _rigidbody = default;

    // プレイヤーと自分(エネミー)の中間距離
    private float _distanceToTarget = default;

    // 現在のプレイヤーの位置(記録用変数)
    private float _currentDistance = default;

    // 過去のプレイヤーの位置(記録用変数)
    private float _pastDistance = default;

    #region ランダム移動関連の変数

    // ターゲットポジション
    private Vector3 _targetPosition = default;

　　// タイマー用変数(変数カウント)
    private float _changeDirectionTimer = default;

    #endregion

    private enum EnemyState
    {

        // ランダムで移動する
        RANGEMOVE,

        // プレイヤーの位置を記憶する
        PLAYERSEARCH,

        // プレイヤーから逃亡する
        ESCAPE,

        // プレイヤーを追跡する
        CHASE,
    }
    /// <summary>
    /// エネミーの行動をEnumで管理
    /// RANGEMOVE    : ランダムで移動する
    /// PLAYERSEARCH : プレイヤーの位置を記憶する
    /// ESCAPE       : プレイヤーから逃亡する
    /// CHASE        : プレイヤーを追跡する
    /// </summary>
    [SerializeField, Header("現在のエネミーの状態")]
    private EnemyState _enemyState = EnemyState.RANGEMOVE;

    #endregion

    #region プロパティ  

    #endregion

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    protected virtual void Update()
    {
        switch(_enemyState)
        {

            case EnemyState.RANGEMOVE:

                print("1.ランダム移動中");

                RangeMove();
                if(_isPlayerSearch)
                {
                    StartCoroutine(nameof(CheckPlayerPos));
                    _isPlayerSearch = false;
                }
                break;

            case EnemyState.PLAYERSEARCH:

                print("2.プレイヤーの位置を探索中");

                PlayerSearch();

                break;

            case EnemyState.CHASE:

                print("3.追跡中");

                Chase();
                break;

            case EnemyState.ESCAPE:

                print("3.逃亡中");

                Escape();
                break;
        }
    }

    #region privateメソッド群  

    /// <summary>
    /// ランダムで移動する
    /// </summary>
    private void RangeMove()
    {

        // 方向を変更するまでの時間を減らす
        _changeDirectionTimer -= Time.deltaTime;

        // 方向を変更する時間に達した場合、新しいターゲットポジションを設定
        if (_changeDirectionTimer <= 0f)
        {
            SetNewRandomTargetPosition();
            _changeDirectionTimer = _enemyData.ChangeDirectionTime;
        }

        // 現在のターゲットポジションに向かって移動
        Vector3 direction = (_targetPosition - transform.position).normalized;
        _rigidbody.MovePosition(transform.position + direction * _enemyData.EnemyMoveSpeed * Time.deltaTime);

        // ターゲットポジションに到達したら新しいターゲットポジションを設定
        if (Vector3.Distance(transform.position, _targetPosition) < _enemyData.TargetReachedThreshold)
        {
            SetNewRandomTargetPosition();
        }
    }

    /// <summary>
    /// 新しいランダムなターゲットポジションを設定する
    /// </summary>
    private void SetNewRandomTargetPosition()
    {

        // 0度から360度の範囲でランダムな角度を取得
        float randomAngle = Random.Range(0f, 360f);

        // 角度をラジアンに変換し、X方向とZ方向の計算を行う
        Vector3 randomDirection = new Vector3(
            Mathf.Cos(randomAngle * Mathf.Deg2Rad),
            0,
            Mathf.Sin(randomAngle * Mathf.Deg2Rad)
        ) * _enemyData.RandomMoveRange;

        // 現在の位置からランダムな方向にターゲットポジションを設定
        _targetPosition = transform.position + randomDirection;

        // ターゲットポジションを特定のグリッドに揃える
        _targetPosition.x = Mathf.Round(_targetPosition.x / 2f) * 2f;
        _targetPosition.z = Mathf.Round(_targetPosition.z / 2f) * 2f;
    }

    /// <summary>
    /// 一定時間後にプレイヤーの探索を行う
    /// </summary>
    private IEnumerator CheckPlayerPos()
    {
        yield return new WaitForSeconds(_enemyData.CheckPlayerPosTime);
        _isPlayerSearch = true;
        _enemyState = EnemyState.PLAYERSEARCH;
    }

    /// <summary>
    /// プレイヤーの位置を記録する
    /// </summary>
    private void PlayerSearch()
    {

        // 現在の位置を過去の位置に保存する
        _pastDistance = _currentDistance;

        // ターゲットとエネミーの中間距離を計算
        _distanceToTarget = Vector3.Distance(transform.position, _rayTargetObj.transform.position);

        // 現在の位置を現在の記録用変数に保存する
        _currentDistance = _distanceToTarget;

        // プレイヤーの位置が過去の位置よりも近かった場合
        if (_pastDistance <= _currentDistance)
        {
            _enemyState = EnemyState.CHASE;
        }
        // プレイヤーの位置が過去の位置よりも遠かった場合
        else if (_pastDistance >= _currentDistance)
        {
            _enemyState = EnemyState.ESCAPE;
        }
    }

    /// <summary>
    /// プレイヤーから逃亡する
    /// </summary>
    private void Escape()
    {

        // プレイヤーから離れる
        Vector3 direction = (_rayTargetObj.transform.position - transform.position).normalized;
        _rigidbody.MovePosition(transform.position - direction * _enemyData.EnemyMoveSpeed * Time.deltaTime);

        StartCoroutine(nameof(EscapeResetState));
    }

    /// <summary>
    /// プレイヤーを追跡する
    /// </summary>
    private void Chase()
    {

        // プレイヤーを追尾
        Vector3 direction = (_rayTargetObj.transform.position - transform.position).normalized;
        _rigidbody.MovePosition(transform.position + direction * _enemyData.EnemyMoveSpeed * Time.deltaTime);

        StartCoroutine(nameof(ChaseResetState));
    }

    /// <summary>
    /// ステートをリセット(初期状態)する
    /// </summary>
    private IEnumerator EscapeResetState()
    {

        yield return new WaitForSeconds(_enemyData.EscapeResetStateTime);

        _enemyState = EnemyState.RANGEMOVE;
    }

    private IEnumerator ChaseResetState()
    {
        yield return new WaitForSeconds(_enemyData.ChaseResetStateTime);

        _enemyState = EnemyState.RANGEMOVE;
    }

    #endregion

    #region publicメソッド群

    /// <summary>
    /// エネミーのRayを制御するメソッド
    /// </summary>
    protected void PlayerTargetingRay()
    {

        if (_rayTargetObj == null)
        {
            Debug.LogWarning("プレイヤーが不在");
            return;
        }

        // 設定したターゲット (_rayTargetObj) にRayを伸ばす
        Vector3 direction = (_rayTargetObj.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        // レイを可視化
        Debug.DrawRay(transform.position, direction * _rayLength, Color.red);

        if (Physics.Raycast(ray, out hit, _rayLength))
        {

            // RayがプレイヤーにHitしていて尚且つ、撃てる状態か
            if (hit.collider.gameObject.CompareTag("Player")&&_isFire)
            {

                _isFire = false;

                // プレイヤーの位置を取得
                _playerPos = hit.transform.position;

                // プレイヤーの位置に向け、弾を撃つ
                Shot();
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
