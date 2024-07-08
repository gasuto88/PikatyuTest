/*-------------------------------------------------
* AttackDataManager.cs
* 
* 作成日　2024/07/
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDataManager : MonoBehaviour
{
    #region フィールド変数

    [SerializeField, Header("通常攻撃データ")]
    private NormalAttackDataAsset[] _normalAttackDatas = default;

    #endregion

    /// <summary>
    /// 通常攻撃データを返す処理
    /// </summary>
    /// <param name="attackNumber">攻撃番号</param>
    /// <returns>攻撃データ</returns>
    public NormalAttackDataAsset ReturnNormalAttackData(int attackNumber)
    {
        return _normalAttackDatas[attackNumber];       
    }

}