// ---------------------------------------------------------  
// PlayerManager.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UniRx;

public class PlayerManager : CharacterManager
{

    #region 変数  

    private PlayerInputManager _playerInputManager = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    #endregion
}
