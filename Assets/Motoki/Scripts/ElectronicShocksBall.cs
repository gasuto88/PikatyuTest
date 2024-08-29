/*-------------------------------------------------
* ElectronicShocksBall.cs
* 
* 作成日　2024/08/
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicShocksBall : Ball 
{
    protected override void Init()
    {
        _ballPool = GameObject.FindGameObjectWithTag("BallPool").GetComponent<ElectronicShocksPool>();
    }

    protected override void UpdateBall()
    {
        _ballMove.MoveElectronicShocksBall(_targetTransform.position,_ballSpeed);

		// 衝突した敵を取得
		Transform targetCharacter = _collisionManager.CollisionTarget(
			_myTransform.position, _myTransform.localScale, _myTransform.rotation, LAYER_ENEMY);

		// 衝突したら弾をしまう
		if (targetCharacter == null)
		{
			return;
		}

		// 敵にぶつかったら
		//if (TryGetComponent<EnemyBase>)
		//{
		//    // ダメージ処理
		//}


		_ballPool.Close(this);
	}
}