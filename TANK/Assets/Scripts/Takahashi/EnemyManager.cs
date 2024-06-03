// ---------------------------------------------------------  
// EnemyManager.cs  
//   
// 作成日:  2024/5/31
// 作成者:  髙橋光栄
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class EnemyManager : CharacterManager,IShootable
{

    #region 変数

    [SerializeField,Tooltip("常に追尾するターゲット")]
    private GameObject _player = default;

    [SerializeField, Tooltip("Rayの長さ")]
    private float _rayLength = 100f;

    // 障害物のレイヤー
    public LayerMask obstacleLayer;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    public void PlayerTargetingRay()
    {
        if (_player == null)
        {
            Debug.LogWarning("Player Transform is not assigned.");
            return;
        }

        Vector3 direction = (_player.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        // レイを可視化
        Debug.DrawRay(transform.position, direction * _rayLength, Color.red);

        if (Physics.Raycast(ray, out hit, _rayLength))
        {

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("プレイヤーにRayがHit!");
                Shot();
            }
            else
            {
                Debug.Log("障害物にRayがHit!");
            }
        }
    }

    /// <summary>
    /// 継承用空メソッド
    /// </summary>
    public virtual void Shot() { }

    #endregion
}
