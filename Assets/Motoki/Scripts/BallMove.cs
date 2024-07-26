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
    public void MoveBall(Vector3 targetPosition,float ballSpeed)
    {
		// 敵の方向を計算
		Vector3 targetDirection = targetPosition - _myTransform.position;

		_myTransform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);

		// 敵に向かって移動
		_myTransform.position += _myTransform.forward * ballSpeed * Time.deltaTime;
    }
}