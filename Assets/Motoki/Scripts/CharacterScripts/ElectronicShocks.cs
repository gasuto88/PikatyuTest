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
	#region フィールド変数

	private float _damage = 0f;

    private ElectronicShocksPool _ballPool = default;

	#endregion

	public ElectronicShocks()
    {		
		_ballPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ElectronicShocksPool>();
	}

	public void Init(Vector3 myPosition, Quaternion myRotation,Character myCharacter,Transform targetTransform)
    {
		
    }

	public void Init(
		Vector3 myPosition, Quaternion myRotation
		, Character myCharacter, Transform targetTransform, float ballSpeed)
    {
		// 弾を取り出す
		Ball ball = _ballPool.TakeOut(myPosition, myRotation);
		ball.SetParameter(myCharacter, targetTransform, _damage,ballSpeed);
	}

	public void Execute(Vector3 myPosition, Quaternion myRotation)
    {
		
	}
	public void Exit(Vector3 myPosition, Quaternion myRotation)
    {
		
    }

	public void SetDamage(float damage)
    {
		_damage = damage;
    }
}