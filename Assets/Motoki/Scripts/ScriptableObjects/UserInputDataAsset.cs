/*-------------------------------------------------
* UserInputDataAsset.cs
* 
* 作成日　2024/06/21
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザー入力データクラス
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UserInputData")]
public class UserInputDataAsset : ScriptableObject
{
	#region フィールド変数

	[SerializeField,Header("入力デッドゾーン"),Min(0f)]
	private float _inputDeadZoon = 0.3f;

    [Space(10)]
    [Header("【アクション名】")]

    [SerializeField, Header("移動名")]
    private string _moveActionName = "Move";

    [SerializeField, Header("通常攻撃名")]
    private string _normalAttackName = "NormalAttack";

    [SerializeField, Header("ロール攻撃名")]
    private string _roleAttackName = "RoleAttack";

    [SerializeField, Header("蘇生名")]
    private string _resurrectionName = "Resurrection";

    [SerializeField,Header("掴み/投げ名")]
    private string _holdTriggerName = "HoldTrigger";

    #endregion

    #region プロパティ

    public float InputDeadZoon { get => _inputDeadZoon; set => _inputDeadZoon = value; }

    public string MoveActionName { get => _moveActionName; set => _moveActionName = value; }

    public string NormalAttackName { get => _normalAttackName; set => _normalAttackName = value; }

    public string RoleAttackName { get => _roleAttackName; set => _roleAttackName = value; }

    public string ResurrectionName { get => _resurrectionName; set => _resurrectionName = value; }

    public string HoldTriggerName { get => _holdTriggerName; set => _holdTriggerName = value; }

    #endregion

}