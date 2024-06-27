/*-------------------------------------------------
* Character.cs
* 
* 作成日　2024/06/27
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour 
{
	#region フィールド変数

	private CharacterStatus _characterStatus = default;

	private ActionState _actionState = default;

	private AliveState _aliveState = default;

	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Start () 
	{
		
	}
}