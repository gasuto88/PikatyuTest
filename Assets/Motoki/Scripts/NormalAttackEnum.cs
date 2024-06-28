/*-------------------------------------------------
* NormalAttackState.cs
* 
* 作成日　2024/06/27
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class NormalAttackEnum
{
    public static readonly Dictionary<string, INormalAttack> _normalAttackEnum = new Dictionary<string, INormalAttack>
    {
        {"ElectronicShocks", new ElectronicShocks()},
        {"Punch", new Punch() }
    };

}