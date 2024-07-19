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

/// <summary>
/// 電気ショッククラス
/// </summary>
public class ElectronicShocks : INormalAttack
{
	#region 定数

	private const float BALL_SPEED = 10f;

    #endregion

    #region フィールド変数

    private ElectronicShocksPool _ballPool = default;

	#endregion

	public ElectronicShocks()
    {		
		_ballPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ElectronicShocksPool>();
	}

	public void Init(Vector3 myPosition, Quaternion myRotation,Character myCharacter,Transform targetTransform)
    {
		// 弾を取り出す
		BallMove ball = _ballPool.TakeOut(myPosition,myRotation);
		ball.BallSpeed = BALL_SPEED;
		ball.TargetTransform = targetTransform;
		ball.SetCharacter(myCharacter);
    }

	public void Execute(Vector3 myPosition, Quaternion myRotation)
    {
		
	}
	public void Exit(Vector3 myPosition, Quaternion myRotation)
    {
		
    }
}