/*-------------------------------------------------
* PlayerDataAsset.cs
* 
* 作成日　2024/06/24
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーデータクラス
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerData")]
public class PlayerDataAsset : ScriptableObject
{
	#region フィールド変数

	[SerializeField, Header("名前")]
	private string _playerName = "ゼロ";

	[SerializeField, Header("HP"), Min(0f)]
	private float _playerHp = 10f;

	[SerializeField, Header("防御"), Min(0f)]
	private float _defense = 5f;

	[SerializeField, Header("移動速度"), Min(0f)]
	private float _moveSpeed = 1f;

	[SerializeField, Header("回転速度"), Min(0f)]
	private float _rotationSpeed = 0f;

	[SerializeField, Header("発射速度"), Min(0f)]
	private float _shotSpeed = 1f;

	[SerializeField, Header("ホールド判定距離"), Min(0f)]
	private float _holdDistance = 1f;

	[Space(20)]
	[Header("【通常攻撃】")]

	[SerializeField, Header("通常攻撃方法")]
	private NormalAttackType _normalAttackType = default;

	[SerializeField, Header("通常攻撃距離"), Min(0f)]
	private float _normalAttackDistance = 2f;

	[SerializeField, Header("通常攻撃時間"), Min(0f)]
	private float _normalAttackTime = 1f;

	[SerializeField, Header("通常攻撃速度"), Min(0f)]
	private float _normalAttackSpeed = 5f;

	[SerializeField,Header("通常攻撃ダメージ"), Min(0f)]
	private float _normalAttackDamage = 0f; 

	[Space(20)]
	[Header("【ロール攻撃】")]

	[SerializeField, Header("ロール攻撃時間"), Min(0f)]
	private float _roleAttackTime = 1f;

	[SerializeField, Header("ロール攻撃クールタイム")]
	private float _roleAttackCoolTime = 5f;

	[SerializeField, Header("バーストの半径")]
	private float _burstRadius = 1f;

	#endregion

	#region プロパティ

	public string PlayerName { get => _playerName; set => _playerName = value; }

	public float PlayerHp { get => _playerHp; set => _playerHp = value; }

	public float Defence { get => _defense; set => _defense = value; }

	public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

	public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

	public float ShotPower { get => _shotSpeed; set => _shotSpeed = value; }

	public float HoldDistance { get => _holdDistance; set => _holdDistance = value; }

	public NormalAttackType TypeNormalAttack { get => _normalAttackType; set => _normalAttackType = value; }

	public float NormalAttackDistance { get => _normalAttackDistance; set => _normalAttackDistance = value; }

	public float NormalAttackTime { get => _normalAttackTime; set => _normalAttackTime = value; }

	public float NormalAttackSpeed { get => _normalAttackSpeed; set => _normalAttackSpeed = value; }

	public float NormalAttackDamage { get => _normalAttackDamage; set => _normalAttackDamage = value; }

	public float RoleAttackTime { get => _roleAttackTime; set => _roleAttackTime = value; }

	public float RoleAttackCoolTime { get => _roleAttackCoolTime; set => _roleAttackCoolTime = value; }
	
	public float BurstRadius { get => _burstRadius; set => _burstRadius = value; }

	#endregion
}