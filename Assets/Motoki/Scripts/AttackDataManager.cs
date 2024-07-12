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

/// <summary>
/// 攻撃データ管理クラス
/// </summary>
public class AttackDataManager : MonoBehaviour
{
    #region フィールド変数

    [SerializeField, Header("通常攻撃データ")]
    private NormalAttackDataAsset[] _normalAttackDatas = default;

    [SerializeField,Header("アタックのロールデータ")]
    private RoleAttackDataAsset[] _attackRoleDatas = default;

    [SerializeField, Header("ディフェンスのロールデータ")]
    private RoleAttackDataAsset[] _defenceRoleDatas = default;

    [SerializeField, Header("サポートのロールデータ")]
    private RoleAttackDataAsset[] _supportRoleDatas = default;

    [SerializeField, Header("バランスのロールデータ")]
    private RoleAttackDataAsset[] _balanceRoleDatas = default;

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

    public RoleAttackDataAsset ReturnRoleAttackData(Character roleType,int attackNumber)
    {
        switch (roleType)
        {
            case Attack:
                {
                    return _attackRoleDatas[attackNumber];                   
                }
            case Defence:
                {
                    return _defenceRoleDatas[attackNumber];
                }
            case Support:
                {
                    return _supportRoleDatas[attackNumber];
                }
            case Balance:
                {
                    return _balanceRoleDatas[attackNumber];
                }
            default:
                {

                }
                break;
        }

        return null;
    }
}