// ---------------------------------------------------------  
// CharacterMine.cs  
//   
// 作成日:  6/12
// 作成者:  山田智哉
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public class CharacterMine : MonoBehaviour
{

    #region 変数  

    // 設置数
    protected int _setCount = default;

    #endregion

    #region publicメソッド群

    /// <summary>
    /// 地雷設置
    /// </summary>
    /// <param name="maxMineSet">最大設置数</param>
    public void MineSetting(int maxMineSet)
    {
        // 設置上限以上はリターン
        if(_setCount >= maxMineSet)
        {
            return;
        }
        
        // 地雷をプールから借り、取得
        PoolObject mineObject = ObjectPoolController.instance.Lend(transform.position, Quaternion.identity, 0);
        
        // 地雷の破棄イベントを購読
        mineObject.OnDestroy.Subscribe(_ => 
        {
            // 設置数を減算
            _setCount -= 1;
        }).AddTo(this);

        // 設置数を加算
        _setCount++;
    }

    #endregion
}
