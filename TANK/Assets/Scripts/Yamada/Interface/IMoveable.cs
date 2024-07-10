// ---------------------------------------------------------  
// IMoveable.cs  
//   
// 作成日:  5/31
// 作成者:  山田智哉
// ---------------------------------------------------------  
using UnityEngine;

public interface IMoveable
{
    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="moveDirection">移動方向</param>
    void Move(Vector3 moveDirection);
}
