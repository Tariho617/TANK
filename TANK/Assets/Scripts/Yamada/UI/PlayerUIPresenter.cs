// ---------------------------------------------------------  
// PlayerUIPresenter.cs  
//   
// 作成日:  6/10  
// 作成者:  山田智哉
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public class PlayerUIPresenter : MonoBehaviour
{

    #region 変数  

    private PlayerShot _playerShot = default;

    private PlayerUIView _playerUIView = default;

    const string PLAYER_TAG = "Player";

    #endregion
  
    #region プロパティ  
  
    #endregion
    
    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    private void Awake()
    {
        _playerShot = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<PlayerShot>();
        _playerUIView = GetComponent<PlayerUIView>();

        _playerShot.TargetDirection.Subscribe(sightPosition => _playerUIView.PlayerPointer(sightPosition, _playerShot.transform.position));
    }
  
}
