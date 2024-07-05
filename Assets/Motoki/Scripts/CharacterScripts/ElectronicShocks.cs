/*-------------------------------------------------
* ElectronicShocks.cs
* 
* 作成日　2024/06/27
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicShocks : INormalAttack
{
	#region 定数

	private const float ATTACK_TIME = 1f;

    #endregion

    #region フィールド変数

    private BallPool _ballPool = default;

	private BallMove _ball = default;

	#endregion

	public ElectronicShocks()
    {		
		_ballPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BallPool>();
	}

	public void Init(Vector3 myPosition, Quaternion myRotation)
    {
		// 弾を取り出す
		_ball = _ballPool.TakeOut(myPosition,myRotation);
    }

	public void Execute(Vector3 myPosition, Quaternion myRotation)
    {
		_ball.MoveBall();			
	}
	public void Exit(Vector3 myPosition, Quaternion myRotation)
    {
		// 弾をしまう
		_ballPool.Close(_ball);
    }
}