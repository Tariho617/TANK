// ---------------------------------------------------------  
// PlayerManager.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UniRx;

public class PlayerManager : CharacterManager, IShootable, IMoveable
{

    #region 変数  

    private PlayerInputManager _playerInputManager = default;
    private CharacterMove _move = default;


    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
        _move = GetComponent<CharacterMove>();
        _playerInputManager.MoveInput.Subscribe(_ => Move(_));
        _playerInputManager.ShotInput.Subscribe(_ => Shot());

        
    }


    public void Move(Vector3 moveDirection)
    {
        _move.MoveDirection(moveDirection);
    }

    public void Shot()
    {
        
    }

    

    #endregion
}
