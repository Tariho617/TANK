// ---------------------------------------------------------  
// NameDisplay.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using Photon.Pun;
using TMPro;

public class NameDisplay : MonoBehaviourPunCallbacks
{
    #region 変数
    private TMP_Text _nameLabel = default;
    #endregion

    #region メソッド  
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        _nameLabel = GetComponent<TMP_Text>();
        // プレイヤー名とプレイヤーIDを表示する
        _nameLabel.text = $"{photonView.Owner.NickName}({photonView.OwnerActorNr})";
    }
    #endregion
}
