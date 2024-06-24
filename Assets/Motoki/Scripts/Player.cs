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

	[SerializeField,Header("プレイヤーデータ")]
	private PlayerDataAsset _playerDataAsset = default;

	// プレイヤー座標
	private Vector3 _playerPosition = default;

	// 自身のTransform
	private Transform _myTransform = default;

	private Move _move = default;

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
		_move = GetComponent<Move>();
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
		Vector2 moveInput = _playerInput.MoveInput;

		// 無入力だったら
		if(moveInput == Vector2.zero)
        {
			return;
        }

		// 移動方向を計算
		Vector3 moveDerection = Vector3.forward * moveInput.y + Vector3.right * moveInput.x;

		//_move.RotateMoveDirection(moveDerection);

		//_move.CalculationMove(moveDerection);

		// 移動を設定
		_myTransform.position = _playerPosition;
    }

	

	/// <summary>
	/// 通常攻撃
	/// </summary>
	private void NormalAttack()
    {

    }
}