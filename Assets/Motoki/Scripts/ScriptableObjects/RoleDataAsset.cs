/*-------------------------------------------------
* RoleDataAsset.cs
* 
* 作成日　2024/07/
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RoleDataAsset")]
public class RoleDataAsset : ScriptableObject
{
	#region フィールド変数

	[SerializeField,Header("ロール攻撃距離"),Min(0f)]
	protected float _roleAttackDistance = 5f;

	[SerializeField, Header("ロール時間"), Min(0f)]
	protected float _roleAttackTime = 1f;

	[SerializeField, Header("ロールクールタイム"), Min(0f)]
	protected float _roleAttackCoolTime = 5f;

	[SerializeField, Header("バーストの半径"), Min(0f)]
	protected float _burstRadius = 1f;

	[SerializeField,Header("バーストダメージ"), Min(0f)]
	protected float _burstDamage = 30f;

	#endregion

	#region プロパティ

	public float RoleAttackDistance { get => _roleAttackDistance; set => _roleAttackDistance = value; }

	public float RoleAttackTime { get => _roleAttackTime; set => _roleAttackTime = value; }

	public float RoleAttackCoolTime { get => _roleAttackCoolTime; set => _roleAttackCoolTime = value; }

	public float BurstRadius { get => _burstRadius; set => _burstRadius = value; }

	public float BurstDamage { get => _burstDamage; set => _burstDamage = value; }

	#endregion
}