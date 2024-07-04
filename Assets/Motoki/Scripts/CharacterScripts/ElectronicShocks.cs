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
	#region フィールド変数

	private BallPool _ballPool = default;

	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Start () 
	{
		_ballPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BallPool>();
	}

	public IEnumerator NormalAttack(Vector3 myPosition, Quaternion myRotation)
    {
		BallMove ball = _ballPool.TakeOut(myPosition, myRotation);


		yield return new WaitForSeconds(1f);
		ball.MoveBall();

		

    }
}