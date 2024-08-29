/*-------------------------------------------------
* BallRoleDataAsset.cs
* 
* 作成日　2024/07/
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BallRoleData")]
public class BallRoleDataAsset : RoleDataAsset 
{
    #region フィールド変数

    [SerializeField, Header("弾の速度"), Min(0f)]
    private float _ballSpeed = 0f;

    [SerializeField, Header("弾のダメージ"), Min(0f)]
    private float _ballDamage = 0f;

    [SerializeField, Header("長押し時間"), Min(0f)]
    private float _longPressTime = 0f;

    [SerializeField, Header("キャンセルクールタイム"), Min(0f)]
    private float _cancelCoolTime = 0f;

    #endregion

    #region プロパティ

    public float BallSpeed { get => _ballSpeed; set => _ballSpeed = value; }

    public float BallDamage { get => _ballDamage; set => _ballDamage = value; }

    public float LongPressTime { get => _longPressTime; set => _longPressTime = value; }

    public float CancelCoolTime { get => _cancelCoolTime; set => _cancelCoolTime = value; }

    #endregion
}