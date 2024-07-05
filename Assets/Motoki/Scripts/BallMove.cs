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

	[SerializeField,Header("弾の速度"),Min(0f)]
	private float _ballSpeed = 0f;

	private Transform _myTransform = default;

	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Awake () 
	{
		_myTransform = transform;
	}

    public void MoveBall()
    {
		_myTransform.position += _myTransform.forward * _ballSpeed * Time.deltaTime;
    }
}