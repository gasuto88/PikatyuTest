/*-------------------------------------------------
* ElectricBall.cs
* 
* 作成日　2024/07/
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBall : IRoleAttack
{
    #region 定数

    private const float BALL_SPEED = 10f;

    #endregion

    #region フィールド変数

    private ElectricBallPool _ballPool = default;

    #endregion

    public ElectricBall()
    {
        _ballPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ElectricBallPool>();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Init(Vector3 myPosition, Quaternion myRotation, Character myCharacter, Transform targetTransform)
    {
        // 弾を取り出す
        BallMove ball = _ballPool.TakeOut(myPosition, myRotation);
        ball.BallSpeed = BALL_SPEED;
        ball.TargetTransform = targetTransform;
        ball.ShotCharacter = myCharacter;
    }
    
    /// <summary>
    /// 実行処理
    /// </summary>
    public void Execute(Vector3 myPosition, Quaternion myRotation)
    {

    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit(Vector3 myPosition, Quaternion myRotation)
    {

    }

}