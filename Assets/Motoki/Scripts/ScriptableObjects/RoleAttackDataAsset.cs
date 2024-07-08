/*-------------------------------------------------
* RoleAttackDataAsset.cs
* 
* 作成日　2024/07/
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAttackDataAsset : ScriptableObject
{
	#region フィールド変数

	[SerializeField, Header("ロール攻撃時間"), Min(0f)]
	protected float _roleAttackTime = 1f;

	[SerializeField, Header("ロール攻撃クールタイム")]
	protected float _roleAttackCoolTime = 5f;

	[SerializeField, Header("バーストの半径")]
	protected float _burstRadius = 1f;

	#endregion

	#region プロパティ

	public float RoleAttackTime { get => _roleAttackTime; set => _roleAttackTime = value; }

	public float RoleAttackCoolTime { get => _roleAttackCoolTime; set => _roleAttackCoolTime = value; }

	public float BurstRadius { get => _burstRadius; set => _burstRadius = value; }

	#endregion
}