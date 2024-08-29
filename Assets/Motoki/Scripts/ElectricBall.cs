/*-------------------------------------------------
* ElectricBall.cs
* 
* 作成日　2024/08/
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBall : Ball
{
    protected override void Init()
    {
        _ballPool = GameObject.FindGameObjectWithTag("BallPool").GetComponent<ElectricBallPool>();
    }

    protected override void UpdateBall()
    {
        _ballMove.MoveElectricBall(_ballSpeed);

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
		
		// バースト範囲内の敵を取得
		 Collider[] hitColliders  = _collisionManager.TargetInBurst(
			targetCharacter.position,_burstRadius, LAYER_ENEMY);

		// 衝突してたらダメージを与える
		if(1 <= hitColliders.Length
		&& hitColliders[0].name != targetCharacter.name)
        {
			Debug.Log("敵衝突");
            foreach (Collider item in hitColliders)
            {
				// 自分に衝突してたらスキップ
				if(item.name == targetCharacter.name)
                {
					continue;
                }
				// ダメージ処理
            }
        }

		// 弾をしまう
		_ballPool.Close(this);
	}
}