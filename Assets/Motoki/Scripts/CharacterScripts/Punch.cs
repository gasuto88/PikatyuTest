/*-------------------------------------------------
* Punch.cs
* 
* 作成日　2024/06/27
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : INormalAttack
{
	#region フィールド変数
	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Start () 
	{
		
	}

	public IEnumerator NormalAttack(Vector3 myPosition, Quaternion myRotation)
    {
		Debug.Log("パンチ");

		yield return new WaitForSeconds(1f);

		
    }
}