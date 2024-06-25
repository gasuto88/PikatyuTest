/*-------------------------------------------------
* MoveCalculator.cs
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
/// 移動計算クラス
/// </summary>
public class MoveCalculator
{
	/// <summary>
	/// 回転計算処理
	/// </summary>
	/// <param name="myRotate"></param>
	/// <param name="moveDirection"></param>
	/// <param name="rotationSpeed"></param>
	/// <returns></returns>
	public Quaternion CalculationRotate(Quaternion myRotate,Vector3 moveDirection,float rotationSpeed)
	{
		// 移動方向まで回転
		Quaternion playerRotate = Quaternion.LookRotation(moveDirection, Vector3.up);

		// 回転を設定
		playerRotate = Quaternion.Slerp(myRotate, playerRotate, rotationSpeed * Time.deltaTime);

		return playerRotate;
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