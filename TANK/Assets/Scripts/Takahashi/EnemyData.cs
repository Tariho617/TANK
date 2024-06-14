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

    [SerializeField][Header("エネミーの移動速度")]
    private float _enemyMoveSpeed = default;

    #region プロパティ

    public float EnemyShotCoolTime { get => _enemyShotCoolTime;}
    public float EnemyMoveSpeed { get => _enemyMoveSpeed;}

    #endregion
}
