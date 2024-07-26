/*-------------------------------------------------
* 作成日　2024/07/12
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoleAttack
{
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Init(Vector3 myPosition, Quaternion myRotation, Character myCharacter, Transform targetTransform);

    /// <summary>
    /// 実行処理
    /// </summary>
    void Execute(Vector3 myPosition, Quaternion myRotation);

    /// <summary>
    /// 終了処理
    /// </summary>
    void Exit(Vector3 myPosition, Quaternion myRotation);

    void SetDamage(float damage);
}