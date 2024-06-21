// ---------------------------------------------------------  
// CharacterShot.cs  
//   
// 作成日:  6/10
// 作成者:  山田智哉
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public abstract class CharacterShot : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("砲台")]
    protected Transform _battery = default;

    // 自機から狙う方向
    protected ReactiveProperty<Vector3> _targetDirection = new();

    // 連射数
    protected int _rapidFireCount = 0;

    // 弾の発射位置調整定数
    protected const float SHOT_POSITION_DISTANCE = 6f;
    protected const float SHOT_POSITION_HEIGHT = 5f;

    #endregion

    #region プロパティ  

    public IReadOnlyReactiveProperty<Vector3> TargetDirection => _targetDirection;

    #endregion

    #region publicメソッド群

    /// <summary>
    /// 射撃
    /// </summary>
    /// <param name="maxRapidFire">最大連射数</param>
    public abstract void Shooting(CharacterData.BulletType shotBulletType, int maxRapidFire, float coolTime);

    #endregion
}
