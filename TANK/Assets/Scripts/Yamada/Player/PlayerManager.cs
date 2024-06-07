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
    private CharacterShot _shot = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    private void Start()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();

        if (TryGetComponent(out _move))
        {
            _playerInputManager.MoveInput.Subscribe(moveDirection => Move(moveDirection));
        }

        if (TryGetComponent(out _shot))
        {
            _playerInputManager.ShotInput.Subscribe(_ => Shot());
        }
    }


    public void Move(Vector3 moveDirection)
    {
        _move.MoveDirection(moveDirection);
    }

    public void Shot()
    {
        _shot.Shooting();
    }

    

    #endregion
}
