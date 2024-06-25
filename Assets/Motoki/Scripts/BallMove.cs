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
	private void Start () 
	{
		_myTransform = transform;
	}

    public void MoveBall(Vector3 moveDirection)
    {
		_myTransform.position += moveDirection * Time.deltaTime;
    }
}