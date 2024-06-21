/*-------------------------------------------------
* Player.cs
* 
* 作成日　2024/06/18
* 更新日　2024/06/18
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤークラス
/// </summary>
public class Player : MonoBehaviour 
{
	#region フィールド変数

	[SerializeField,Header("名前")]
	private string _playerName = "ゼロ";

	[SerializeField, Header("HP"), Min(0f)]
	private float _playerHp = 10f;

	[SerializeField,Header("防御"), Min(0f)]
	private float _defense = 5f;

	[SerializeField, Header("移動速度"), Min(0f)]
	private float _moveSpeed = 1f;

	[SerializeField,Header("回転速度"),Min(0f)]
	private float _rotationSpeed = 0f;

	[SerializeField, Header("発射威力"), Min(0f)]
	private float _shotPower = 1f;

	[SerializeField, Header("ホールド判定距離"), Min(0f)]
	private float _holdDistance = 1f;

	[SerializeField, Header("通常攻撃距離"), Min(0f)]
	private float _normalAttackDistance = 2f;

	[SerializeField, Header("攻撃時間"), Min(0f)]
	private float _attackTime = 1f;

	[SerializeField, Header("ロール攻撃時間"), Min(0f)]
	private float _roleAttackTime = 1f;

	[SerializeField,Header("ロール攻撃クールタイム")]
	private float _roleAttackCoolTime = 5f;

	[SerializeField,Header("バーストの半径")]
	private float _burstRadius = 1f;

	// プレイヤー座標
	private Vector3 _playerPosition = default;

	// 自身のTransform
	private Transform _myTransform = default;

	// プレイヤー入力クラス
	private UserInput _playerInput = default;



    #endregion

	/// <summary>
	/// 更新前処理
	/// </summary>
    private void Start()
    {
		// 自身のTransformを設定
		_myTransform = transform;

		// 自身の座標を設定
		_playerPosition = _myTransform.position;

		// Script取得
		_playerInput = GetComponent<UserInput>();
	}

	/// <summary>
	/// 更新処理
	/// </summary>
    private void Update()
    {
        UpdatePlayer();
    }

    /// <summary>
    /// プレイヤー更新処理
    /// </summary>
    public void UpdatePlayer()
    {
        
        if (_playerInput.IsNormalAttack)
		{
            Debug.Log("通常攻撃");
        }

		// 移動の入力を取得
		Vector2 moveInput = _playerInput.Move();

		// 無入力だったら
		if(moveInput == Vector2.zero)
        {
			return;
        }

		// 移動方向を計算
		Vector3 moveDerection = Vector3.forward * moveInput.y + Vector3.right * moveInput.x;

		RotateMoveDirection(moveDerection);

		Move(moveDerection);

		// 移動を設定
		_myTransform.position = _playerPosition;
    }

	/// <summary>
	/// 移動方向に回転する処理
	/// </summary>
	/// <param name="moveDirection">移動方向</param>
	private void RotateMoveDirection(Vector3 moveDirection)
	{
		// 移動方向まで回転
		Quaternion playerRotate = Quaternion.LookRotation(moveDirection,Vector3.up);

		// 回転を設定
		_myTransform.rotation = Quaternion.Slerp(_myTransform.rotation,playerRotate,_rotationSpeed * Time.deltaTime);
	}

	/// <summary>
	/// 移動処理
	/// </summary>
	/// <param name="moveDirection">移動方向</param>
	private void Move(Vector3 moveDirection)
    {
		_playerPosition += moveDirection * _moveSpeed * Time.deltaTime;
    }
}