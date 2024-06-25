/*-------------------------------------------------
* Player.cs
* 
* 作成日　2024/06/18
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

	[SerializeField,Header("プレイヤーデータ")]
	private PlayerDataAsset _playerDataAsset = default;

	// プレイヤー座標
	private Vector3 _playerPosition = default;

	// プレイヤーのQuaternion
	private Quaternion _playerQuaternion = default;

	// 移動方向
	private Vector3 _moveDirection = default;

	// 自身のTransform
	private Transform _myTransform = default;

	// 攻撃クラス
	private Attack _attack = default;

	// 移動クラス
	private MoveCalculator _moveCalculator = default;

	// プレイヤー入力クラス
	private UserInput _userInput = default;


    #endregion

    #region プロパティ

	public Vector3 PlayerPosition { get => _playerPosition; }

	public Quaternion PlayerRotation { get => _playerQuaternion; }

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

		// 自身の回転角度を設定
		_playerQuaternion = _myTransform.rotation;

		// Script取得
		_userInput = GetComponent<UserInput>();
		_attack = GetComponent<Attack>();
		_moveCalculator = new();
		
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
		if (_userInput.IsNormalAttack)
		{
			
		}
		else if (_userInput.IsRoleAttack)
		{

		}
		else if (_userInput.IsResurrection)
        {

        }
		else if (_userInput.IsHoldTrigger)
        {

        }

		// 移動の入力を取得
		Vector2 moveInput = _userInput.MoveInput;

		// 移動方向を計算
		_moveDirection = Vector3.forward * moveInput.y + Vector3.right * moveInput.x;
		
		// 回転を計算
		_playerQuaternion = _moveCalculator.CalculationRotate(_myTransform.rotation, _moveDirection,_playerDataAsset.RotationSpeed);
		
		// 無入力だったら
		if (moveInput != Vector2.zero)
        {
			// 移動量を加算
			_playerPosition += _moveCalculator.CalculationMove(_moveDirection,_playerDataAsset.MoveSpeed);
		}

		// 回転角度を設定
		_myTransform.rotation = _playerQuaternion;

		// 移動を設定
		_myTransform.position = _playerPosition;
    }
}