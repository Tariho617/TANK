// ---------------------------------------------------------  
// Mine.cs  
//   
// 作成日:  6/11
// 作成者:  山田智哉
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class Mine : PoolObject, IDamageable
{
    #region 変数  

    #endregion

    #region プロパティ  

    #endregion


    #region privateメソッド群  
    protected override void HideObject()
    {
        base.HideObject();
    }
    #endregion

    #region publicメソッド群
    public override void AppearanceObject(int poolObjectNumber)
    {
        base.AppearanceObject(poolObjectNumber);
    }

    public void ReceiveDamage()
    {
        HideObject();
    }
    #endregion
}
