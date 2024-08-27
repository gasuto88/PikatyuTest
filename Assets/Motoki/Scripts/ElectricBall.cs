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
        if (targetCharacter != null)
        {
            _ballPool.Close(this);
        }
    }
}