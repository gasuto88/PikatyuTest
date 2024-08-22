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

    #endregion

    #region プロパティ

    public float BallSpeed { get => _ballSpeed; set => _ballSpeed = value; }

    #endregion
}