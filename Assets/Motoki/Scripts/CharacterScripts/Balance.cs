/*-------------------------------------------------
* Balance.cs
* 
* 作成日　2024/06/25
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : Character
{
	#region フィールド変数

	[SerializeField, Header("ロール攻撃方法")]
	private BalanceRoleType _balanceRoleType = default;

	#endregion
}