/*-------------------------------------------------
* 作成日　2024/06/27
*
* 作成者　本木大地
-------------------------------------------------*/
using System.Collections;
using UnityEngine;

public interface INormalAttack
{
     IEnumerator NormalAttack(Vector3 myPosition,Quaternion myRotation);
}