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
}