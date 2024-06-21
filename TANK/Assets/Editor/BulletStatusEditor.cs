// ---------------------------------------------------------  
// BulletStatusEditor.cs  
//   
// 作成日:  6/20
// 作成者:  山田智哉
// ---------------------------------------------------------
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BulletStatus))]
public class BulletStatusEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // ターゲットとなるBulletStatusのインスタンスを取得
        BulletStatus bulletStatus = (BulletStatus)target;

        // プロパティの表示
        EditorGUILayout.LabelField("弾ステータス", EditorStyles.boldLabel);

        bulletStatus._bulletData._speed = EditorGUILayout.FloatField("弾速", bulletStatus._bulletData._speed);
        bulletStatus._bulletData._lifeTime = EditorGUILayout.FloatField("生存時間", bulletStatus._bulletData._lifeTime);
        bulletStatus._bulletData._reflections = EditorGUILayout.IntField("反射回数", bulletStatus._bulletData._reflections);

        // 変更があった場合の処理
        if (GUI.changed)
        {
            EditorUtility.SetDirty(bulletStatus);
        }
    }
}