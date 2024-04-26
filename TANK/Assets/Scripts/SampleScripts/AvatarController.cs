// ---------------------------------------------------------  
// AvatarController.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using Photon.Pun;
using System.Collections;

public class AvatarController : MonoBehaviourPunCallbacks
{
    #region メソッド  
     /// <summary>  
     /// 初期化処理  
     /// </summary>  
     void Awake()
     {
     }
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
  
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     private void Update ()
     {
        var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        if (input.sqrMagnitude > 0f)
        {
            transform.Translate(6f * Time.deltaTime * input.normalized);
        }
     }
  
    #endregion
}
