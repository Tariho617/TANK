// ---------------------------------------------------------  
// GameManager.cs  
//   
// 作成日:  2024.7.24
// 作成者:  髙橋光栄
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using Unity.AI.Navigation;
public class GameManager : MonoBehaviour
{

    // ナビメッシュ登録用
    private NavMeshSurface navMeshSurface;

    #region 変数  

    #endregion

    #region プロパティ  

    #endregion

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    private void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }
  
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        StartCoroutine(nameof(tamesi));
    }
  
    private IEnumerator tamesi()
    {
        yield return new WaitForSeconds(1f);
        navMeshSurface.layerMask = LayerMask.GetMask("Default");
        // ナビメッシュをベイクする
        navMeshSurface.BuildNavMesh();

        print("ベイクしたお♡");
    }


    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {

    }

    #region privateメソッド群  
  
    #endregion

    #region publicメソッド群

    #endregion
}
