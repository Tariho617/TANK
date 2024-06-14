// ---------------------------------------------------------  
// CharacterManager.cs  
//   
// 作成日:  5/31
// 作成者:  山田智哉
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour, IDamageable
{

    #region 変数  

    [SerializeField, Tooltip("キャラクターのステータス")]
    protected CharacterStatus _characterStatus = default;
    protected CharacterData _characterData = default;

    #endregion

    #region プロパティ  

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        _characterData = _characterStatus._characterData;
    }

    #region privateメソッド群

    #endregion

    #region publicメソッド群

    public void ReceiveDamage()
    {
        
    }

    #endregion
}
