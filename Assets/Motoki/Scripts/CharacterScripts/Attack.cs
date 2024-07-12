/*-------------------------------------------------
* Attack.cs
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

public class Attack : Character
{
	#region フィールド変数

	[SerializeField, Header("ロール攻撃方法")]
	private AttackRoleType _attackRoleType = default;

    #endregion

    protected override void Init()
    {
        _roleAttackData = _skillManager.ReturnRoleAttackData(this, (int)_attackRoleType);
        _iRoleAttack = RoleAttackEnum._roleAttackEnum.Values.ToArray()[(int)_normalAttackType];
    }
}