// ---------------------------------------------------------  
// CharacterShot.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class CharacterShot : MonoBehaviour
{

    #region 変数  

    [SerializeField]
    protected GameObject _battery = default;

    // 自機から狙う位置の方向
    protected Vector3 _targetDirection = default;

    // 連射数
    protected int _rapidFireCount = 0;

    // 弾の発射位置調整定数
    protected const float SHOT_POSITION_DISTANCE = 4f;

    // 最大連射数
    protected const int RAPID_FIRE_LIMIT = 5;

    // 連射クールタイム
    protected const float RAPID_FIRE_COOLTIME = 3f;

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
    private void Update ()
    {

    }

    #region privateメソッド群  

    #endregion

    #region publicメソッド群

    /// <summary>
    /// 射撃
    /// </summary>
    public abstract void Shooting();

    #endregion
}
