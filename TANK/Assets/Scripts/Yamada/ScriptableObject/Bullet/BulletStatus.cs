// ---------------------------------------------------------  
// BulletStatus.cs  
//   
// 作成日: 6/11 
// 作成者: 山田智哉
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "ScriptableObject/CreateBulletStatus")]
public class BulletStatus : ScriptableObject
{
    public BulletData _bulletData = default;
}
