/*-------------------------------------------------
* Defence.cs
* 
* 作成日　2024/06/25
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Defence : Character
{
	#region フィールド変数

	[SerializeField, Header("ロール攻撃方法")]
	private DefenceRoleType _defenceRoleType = default;

	#endregion

	protected override void Init()
	{
		_roleAttackData = _skillManager.ReturnRoleAttackData(this, (int)_defenceRoleType);
		_iRoleAttack = RoleAttackEnum._roleAttackEnum.Values.ToArray()[(int)_defenceRoleType];
	}
}