/*-------------------------------------------------
* 作成日　2024/06/27
*
* 作成者　本木大地
-------------------------------------------------*/
using System.Collections;
using UnityEngine;

public interface INormalAttack
{
    void Init(Vector3 myPosition, Quaternion myRotation);

    void Execute(Vector3 myPosition, Quaternion myRotation);

    void Exit(Vector3 myPosition, Quaternion myRotation);
}