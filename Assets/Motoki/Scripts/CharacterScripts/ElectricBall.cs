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
    #region フィールド変数

    private float _damage = 0f;

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
        Ball ball = _ballPool.TakeOut(myPosition, myRotation);
        //ball.SetParameter(myCharacter, targetTransform,_damage);
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

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

}