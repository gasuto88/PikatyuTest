/*-------------------------------------------------
* Character.cs
* 
* 作成日　2024/06/27
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour 
{
	#region フィールド変数

	// プレイヤー座標
	protected Vector3 _playerPosition = default;

	// 移動方向
	protected Vector3 _moveDirection = default;

	// プレイヤーのQuaternion
	protected Quaternion _playerQuaternion = default;

	// プレイヤー入力クラス
	protected UserInput _userInput = default;

	// 自身のTransform
	protected Transform _myTransform = default;

	// キャラクターステータス
	[SerializeField]
	protected CharacterStatus _characterStatus = default;

	[SerializeField]
	protected float _speed = default;

	// 行動状態
	protected ActionState _actionState = default;

	// 生存状態
	[SerializeField]
	protected AliveState _aliveState = default;

	[SerializeField]
	private INormalAttack _a = default;

	#endregion

	#region プロパティ

	public Vector3 PlayerPosition { get => _playerPosition; }

	public Quaternion PlayerRotation { get => _playerQuaternion; }

	#endregion

	/// <summary>
	/// 更新前処理
	/// </summary>
	private void Start () 
	{
		_a = NormalAttackEnum._normalAttackEnum.Values.ToArray()[0];

		Debug.Log(_a);
		_a.NormalAttack();
		// 自身のTransformを設定
		_myTransform = transform;

		// 自身の座標を設定
		_playerPosition = _myTransform.position;

		// 自身の回転角度を設定
		_playerQuaternion = _myTransform.rotation;

		// Script取得
		_userInput = GetComponent<UserInput>();

		_characterStatus._moveSpeed = 0f;
	}


	/// <summary>
	/// キャラクターを更新する処理
	/// </summary>
	private void UpdateCharacter()
    {
		// 移動の入力を取得
		Vector2 moveInput = _userInput.MoveInput;

		// 移動方向を計算
		_moveDirection = Vector3.forward * moveInput.y + Vector3.right * moveInput.x;

		// 回転を計算
		//_playerQuaternion = CalculationRotate(_myTransform.rotation, _moveDirection, _playerDataAsset.RotationSpeed);

		// 無入力だったら
		if (moveInput != Vector2.zero)
		{
			// 移動量を加算
			_playerPosition += CalculationMove(_moveDirection, _characterStatus._moveSpeed);
		}

		// 回転角度を設定
		_myTransform.rotation = _playerQuaternion;

		// 移動を設定
		_myTransform.position = _playerPosition;
	}



	/// <summary>
	/// 移動計算処理
	/// </summary>
	/// <param name="moveDirection">移動方向</param>
	/// <param name="moveSpeed">移動速度</param>
	/// <returns>移動量</returns>
	public Vector3 CalculationMove(Vector3 moveDirection, float moveSpeed)
	{
		Vector3 moveVector = moveDirection * moveSpeed * Time.deltaTime;

		return moveVector;
	}
}
