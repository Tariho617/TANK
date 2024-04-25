// ---------------------------------------------------------  
// AvatarPrefabPool.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class AvatarPrefabPool : MonoBehaviour, IPunPrefabPool
{

    #region 変数  
    /// <summary>
    /// アバタープレハブを格納する
    /// </summary>
    [SerializeField]
    [Header("Avatarプレハブを設定する"), Tooltip("Avatar")]
    private GameObject _avatarPrefab = default;

    /// <summary>
    /// シーンへ表示・非アクティブ化するアバターを格納する
    /// </summary>
    private GameObject _avatar = default;

    /// <summary>
    /// 生成されたアバタープレハブを格納する
    /// </summary>
    private Stack<GameObject> _inactiveObjectPool = new Stack<GameObject>();
    #endregion

    #region メソッド  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     private void Start ()
     {
        // ネットワークオブジェクトの生成・破棄を行う処理を、このクラスの処理に差し替える
        PhotonNetwork.PrefabPool = this;
     }
     
     GameObject IPunPrefabPool.Instantiate(string prefabId, Vector3 position, Quaternion rotation)
     {
        switch (prefabId)
        {
            case "Avatar":
                //スタック内にオブジェクトがある場合
                if(_inactiveObjectPool.Count > 0)
                {
                    //ポップしてトランスフォームを設定する
                    _avatar = _inactiveObjectPool.Pop();
                    _avatar.transform.SetPositionAndRotation(position, rotation);
                }
                //スタック内に無かった場合
                else
                {
                    //生成後、非アクティブ状態にする
                    //ネットワークオブジェクトは非アクティブで返す必要があるため
                    _avatar = Instantiate(_avatarPrefab, position, rotation);
                    _avatar.gameObject.SetActive(false);
                }
                //アバターのゲームオブジェクトを返す
                return _avatar.gameObject;
        }
        return null;
     }
     
     /// <summary>
     /// アバターをプールへ返す
     /// SetActiveの処理はPhotonNetworkの内部で行われているので省略
     /// </summary>
     /// <param name="gameObject">非アクティブにするアバター</param>
     void IPunPrefabPool.Destroy(GameObject gameObject)
     {
        //コンポーネント取得
        _avatar = gameObject.GetComponent<GameObject>();
        //非アクティブ化したアバターをプッシュする
        _inactiveObjectPool.Push(_avatar);
     }
    #endregion
}
