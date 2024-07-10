// ---------------------------------------------------------  
// PlayerInputManager.cs  
//   
// 作成日:  5/31
// 作成者:  山田智哉
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UniRx;
using System;

public class PlayerInputManager : MonoBehaviour
{

    #region 変数  

    protected ReactiveProperty<Vector3> _moveInput = new();
   
    protected Subject<Unit> _shotInput = new();

    protected Subject<Unit> _mineInput = new();

    #endregion

    #region プロパティ 

    public IReadOnlyReactiveProperty<Vector3> MoveInput => _moveInput;

    public IObservable<Unit> ShotInput => _shotInput;

    public IObservable<Unit> MineInput => _mineInput;

    #endregion

    #region メソッド  

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput.Value = context.ReadValue<Vector2>();
    }

    public void OnShot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _shotInput.OnNext(Unit.Default);
        }
    }

    public void OnMine(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _mineInput.OnNext(Unit.Default);
        }
    }

    #endregion
}
