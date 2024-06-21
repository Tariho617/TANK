// ---------------------------------------------------------  
// CharacterStatus.cs  
//   
// 作成日:  6/12
// 作成者:  山田智哉
// ---------------------------------------------------------  
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CreateCharacterStatus")]
public class CharacterStatus : ScriptableObject
{
    public CharacterData _characterData = default;
}
