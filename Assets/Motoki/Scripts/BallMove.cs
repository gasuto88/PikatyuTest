/*-------------------------------------------------
* BallMove.cs
* 
* 作成日　2024/06/25
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour 
{
	#region フィールド変数

	private Transform _myTransform = default;

    #endregion


    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Awake () 
	{
		_myTransform = transform;
	}

	/// <summary>
	/// 弾の移動処理
	/// </summary>
    public void MoveElectronicShocksBall(Vector3 targetPosition,float ballSpeed)
    {
		// 弾を回転
		_myTransform.rotation = RotationBall(targetPosition);

		// 敵に向かって移動
		_myTransform.position += _myTransform.forward * ballSpeed * Time.deltaTime;
    }

	public void MoveElectricBall(float ballSpeed)
    {
		// 敵に向かって移動
		_myTransform.position += _myTransform.forward * ballSpeed * Time.deltaTime;
	}

	/// <summary>
	/// 弾回転処理
	/// </summary>
	/// <param name="targetPosition">敵の座標</param>
	/// <returns>回転</returns>
	public Quaternion RotationBall(Vector3 targetPosition)
    {
		// 敵の方向を計算
		Vector3 targetDirection = targetPosition - _myTransform.position;

		return Quaternion.LookRotation(targetDirection, Vector3.up);
	}

}