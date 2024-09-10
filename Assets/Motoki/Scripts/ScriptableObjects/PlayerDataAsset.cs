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
/// プレイヤーデータ
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

	[SerializeField,Header("回転時間"), Min(0f)]
	private float _rotationTime = 0.5f;

	[SerializeField, Header("発射速度"), Min(0f)]
	private float _shotSpeed = 1f;

	[SerializeField, Header("ホールド判定距離"), Min(0f)]
	private float _holdDistance = 1f;

    [SerializeField, Header("ホールドクールタイム"), Min(0f)]
    private float _holdCoolTime = 1f;

    [SerializeField, Header("バーストの半径"), Min(0f)]
    private float _burstRadius = 5f;

    [SerializeField, Header("バーストダメージ"), Min(0f)]
    private float _burstDamage = 30f;

    [SerializeField,Header("つかめる対象")]
	private string[] _holdObjects = default;

    [SerializeField, Header("ぶつかる対象")]
    private string[] _collisionObjects = default;   

    #endregion

    #region プロパティ

    public string PlayerName { get => _playerName; set => _playerName = value; }

	public float PlayerHp { get => _playerHp; set => _playerHp = value; }

	public float Defence { get => _defense; set => _defense = value; }

	public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

	public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

	public float RotationTime { get => _rotationTime; set => _rotationTime = value; }

	public float ShotSpeed { get => _shotSpeed; set => _shotSpeed = value; }

	public float HoldDistance { get => _holdDistance; set => _holdDistance = value; }

	public float HoldCoolTime { get => _holdCoolTime; set => _holdCoolTime = value; }

    public float BurstRadius { get => _burstRadius; set => _burstRadius = value; }

    public float BurstDamage { get => _burstDamage; set => _burstDamage = value; }

    public string[] HoldObjects { get => _holdObjects; set => _holdObjects = value; }

	public string[] ColisionObjects { get => _collisionObjects; set => _collisionObjects = value; }

    #endregion
}