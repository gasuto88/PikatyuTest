/*-------------------------------------------------
* ElectronicShocksData.cs
* 
* 作成日　2024/07/08
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通常攻撃データ
/// </summary>
public class NormalAttackDataAsset : ScriptableObject
{
	#region フィールド変数

	[SerializeField, Header("通常攻撃距離"), Min(0f)]
	protected float _normalAttackDistance = 2f;

	[SerializeField, Header("通常攻撃時間"), Min(0f)]
	protected float _normalAttackTime = 1f;

	[SerializeField, Header("通常攻撃速度"), Min(0f)]
	protected float _normalAttackSpeed = 5f;

	[SerializeField, Header("通常攻撃ダメージ"), Min(0f)]
	protected float _normalAttackDamage = 0f;

	[SerializeField,Header("通常攻撃クールタイム"), Min(0f)]
	protected float _normalAttackCoolTime = 0f;

	#endregion

	#region プロパティ

	public float NormalAttackDistance { get => _normalAttackDistance; set => _normalAttackDistance = value; }

	public float NormalAttackTime { get => _normalAttackTime; set => _normalAttackTime = value; }

	public float NormalAttackSpeed { get => _normalAttackSpeed; set => _normalAttackSpeed = value; }

	public float NormalAttackDamage { get => _normalAttackDamage; set => _normalAttackDamage = value; }

	public float NormalAttackCoolTime { get => _normalAttackCoolTime; set => _normalAttackCoolTime = value; }

    #endregion
}