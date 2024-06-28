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

[CustomEditor(typeof(Character))]
public class NormalAttackRegistration : Editor
{
	#region フィールド変数

	private Character _target = default;

	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	void OnEnable () 
	{

		_target = (Character)target;

	}

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();



    }
}