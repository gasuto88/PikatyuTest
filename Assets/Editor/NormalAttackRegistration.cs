/*-------------------------------------------------
* NormalAttackRegistration.cs
* 
* 作成日　2024/06/27
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Character), true)]
[CanEditMultipleObjects]
public class NormalAttackRegistration : Editor
{
    #region フィールド変数

    private Character _target = default;
    private const string prefabPath = "Assets/Motoki/Prefabs/Player.prefab";

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    void OnEnable()
    {
        _target = (Character)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //// 選択された通常攻撃のインデックスを取得
        //int value = EditorGUILayout.Popup("通常攻撃方法", _target.NormalAttackNumber, NormalAttackEnum._normalAttackEnum.Keys.ToArray());

        //// 前に選択されてたインデックスと違ったら
        //if (value != _target.NormalAttackNumber)
        //{

        //    // Undoシステムに対応するための処理
        //    Undo.RecordObject(_target, "Change MyComponent Values");

        //    _target.NormalAttackNumber = value;
        //    // ターゲットクラスに変更があることを通知
        //    EditorUtility.SetDirty(_target);

        //    // セーブ
        //    AssetDatabase.SaveAssets();


        //    // プレハブアセットを取得
        //    GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource(_target.gameObject) as GameObject;
        //    if (prefab != null)
        //    {
        //        // プレハブを更新
        //        PrefabUtility.SaveAsPrefabAssetAndConnect(_target.gameObject, AssetDatabase.GetAssetPath(prefab), InteractionMode.UserAction);
        //    }
        //    // リフレッシュ
        //    AssetDatabase.Refresh();
        //}
    }
}