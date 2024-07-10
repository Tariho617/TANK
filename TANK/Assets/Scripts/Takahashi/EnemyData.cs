// ---------------------------------------------------------  
// EnemyData.cs  
//   
// 作成日:  2024.6.14
// 作成者:  髙橋光栄
// ---------------------------------------------------------  
using UnityEngine;
[CreateAssetMenu(menuName ="Enemy_Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField][Header("エネミーのショットクールタイム")]
    private float _enemyShotCoolTime = default;

    [SerializeField][Header("エネミーのショットスピード")]
    private float _enemyShotSpped = default;

    [SerializeField][Header("エネミーの移動速度")]
    private float _enemyMoveSpeed = default;

    [SerializeField][Header("エネミーの方向を変更するまでの時間")]
    private float _changeDirectionTime = default;

    [SerializeField][Header("エネミーのランダム移動範囲")]
    private float _randomMoveRange = default;

    [SerializeField][Header("エネミーがターゲットポジションに到達とみなす距離")]
    private float _targetReachedThreshold = default;

    [SerializeField][Header("エネミーの最大ショットオブジェクト生成数(オブジェクトプール)")]
    private int _enemyMaxPoolCount = default;

    [SerializeField][Header("エネミーショットの存在時間")]
    private float _enemyShotDisplayTime = default;

    [SerializeField][Header("エネミーがプレイヤーをサーチする間隔")]
    private float _checkPlayerPosTime = default;

    [SerializeField][Header("逃亡状態をリセットするまでの時間")]
    private float _escapeResetStateTime = default;

    [SerializeField][Header("追跡状態をリセットするまでの時間")]
    private float _chaseResetStateTime = default;

    #region プロパティ

    public float EnemyShotCoolTime { get => _enemyShotCoolTime;}
    public float EnemyMoveSpeed { get => _enemyMoveSpeed;}
    public int EnemyMaxPoolCount { get => _enemyMaxPoolCount;}
    public float EnemyShotDisplayTime { get => _enemyShotDisplayTime;}
    public float EnemyShotSpped { get => _enemyShotSpped;}
    public float CheckPlayerPosTime { get => _checkPlayerPosTime;}
    public float ChangeDirectionTime { get => _changeDirectionTime;}
    public float RandomMoveRange { get => _randomMoveRange;}
    public float TargetReachedThreshold { get => _targetReachedThreshold;}
    public float EscapeResetStateTime { get => _escapeResetStateTime;}
    public float ChaseResetStateTime { get => _chaseResetStateTime;}

    #endregion
}
