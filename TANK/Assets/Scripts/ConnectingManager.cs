// ---------------------------------------------------------  
// ConnectingManager.cs  
//   
// 作成日:  2024年4月25日
// 作成者:  堀田祐太
// ---------------------------------------------------------  
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectingManager : MonoBehaviourPunCallbacks
{
    #region 変数
    /// <summary>
    /// 初期生成位置を格納する
    /// </summary>
    private Vector3 _startPosition = default;
    #endregion

    #region メソッド  
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        PhotonNetwork.NickName = "Player";
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// マスターサーバーへ接続が成功したときに呼ばれるコールバック
    /// </summary>
    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    /// <summary>
    /// ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    /// </summary>
    public override void OnJoinedRoom()
    {

        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        _startPosition = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate("Avatar", _startPosition, Quaternion.identity);
    }
    #endregion
}
