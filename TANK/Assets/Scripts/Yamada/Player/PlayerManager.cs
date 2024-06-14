// ---------------------------------------------------------  
// PlayerManager.cs  
//   
// 作成日:  6/1
// 作成者:  山田智哉
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public class PlayerManager : CharacterManager, IShootable, IMoveable, IMineSettable
{

    #region 変数  

    // 入力クラス
    private PlayerInputManager _playerInputManager = default;
    // 移動クラス
    private CharacterMove _move = default;
    // 射撃クラス
    private CharacterShot _shot = default;
    // 地雷設置
    private CharacterMine _mine = default;

    #endregion

    #region プロパティ  

    #endregion
      
    /// <summary>
    /// 起動時処理
    /// </summary>
    private void Awake()
    {
        // キャッシュ
        _playerInputManager = GetComponent<PlayerInputManager>();

        // 各機能を取得
        if (TryGetComponent(out _move))
        {
            _playerInputManager.MoveInput.Subscribe(moveDirection => Move(moveDirection))
                .AddTo(this);
        }

        if (TryGetComponent(out _shot))
        {
            _playerInputManager.ShotInput.Subscribe(_ => Shot())
                .AddTo(this);
        }

        if (TryGetComponent(out _mine))
        {
            _playerInputManager.MineInput.Subscribe(_ => MineSet())
                .AddTo(this);
        }
    }

    #region publicメソッド群

    public void Move(Vector3 moveDirection)
    {
        _move.Moving(moveDirection, _characterData._moveSpeed);
    }

    public void Shot()
    {
        _shot.Shooting(_characterData._maxRapidFire);
    }

    public void MineSet()
    {
        _mine.MineSetting(_characterData._maxMineSet);
    }

    #endregion
}
