/*-------------------------------------------------
* RoleAttackEnum.cs
* 
* 作成日　2024/07/12
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class RoleAttackEnum
{
    public static readonly Dictionary<string, IRoleAttack> _roleAttackEnum = new Dictionary<string, IRoleAttack>
    {
        {"ElectricBall", new ElectricBall()},     
    };
}