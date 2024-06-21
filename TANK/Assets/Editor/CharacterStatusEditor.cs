// ---------------------------------------------------------  
// CharacterStatusEditor.cs  
//   
// 作成日:  6/20
// 作成者:  山田智哉
// ---------------------------------------------------------  
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterStatus))]
public class CharacterStatusEditor : Editor
{
    /// <summary>
    /// インスペクター表示
    /// </summary>
    public override void OnInspectorGUI()
    {
        // CharacterStatusのインスタンスを取得
        CharacterStatus characterStatus = (CharacterStatus)target;

        // プロパティの表示
        EditorGUILayout.LabelField("ステータス", EditorStyles.boldLabel);

        characterStatus._characterData._maxRapidFire = 
            EditorGUILayout.IntField("最大連射数", characterStatus._characterData._maxRapidFire);

        characterStatus._characterData._maxMineSet = 
            EditorGUILayout.IntField("最大地雷設置数", characterStatus._characterData._maxMineSet);

        characterStatus._characterData._moveSpeed = 
            EditorGUILayout.FloatField("移動速度", characterStatus._characterData._moveSpeed);

        characterStatus._characterData._shotCoolTime = 
            EditorGUILayout.FloatField("射撃クールタイム", characterStatus._characterData._shotCoolTime);

        characterStatus._characterData._shotBulletType = 
            (CharacterData.BulletType)EditorGUILayout.EnumPopup("撃つ弾種", characterStatus._characterData._shotBulletType);

        // 変更があった場合の処理
        if (GUI.changed)
        {
            EditorUtility.SetDirty(characterStatus);
        }
    }
}