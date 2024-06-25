/*-------------------------------------------------
* PlayerInput.cs
* 
* 作成日　2024/06/18
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

/// <summary>
/// ユーザー入力クラス
/// </summary>
public class UserInput : MonoBehaviour
{
    #region フィールド変数

    [SerializeField, Header("ユーザー入力データ")]
    private UserInputDataAsset _userInputDataAsset = default;

    private Vector2 _moveInput = default;

    // 通常攻撃判定
    private bool _isNormalAttack = false;

    // ロール攻撃判定
    private bool _isRoleAttack = false;

    // 蘇生判定
    private bool _isResurrection = false;

    // 掴み/投げ判定
    private bool _isHoldTrigger = false;

    private PlayerInput _playerInput = default;

    #endregion

    #region プロパティ

    public Vector2 MoveInput
    {
        get
        {
            // 入力デッドゾーン
            if (-_userInputDataAsset.InputDeadZoon <= _moveInput.x
                && _moveInput.x <= _userInputDataAsset.InputDeadZoon)
            {
                _moveInput.x = 0f;
            }
            if (-_userInputDataAsset.InputDeadZoon <= _moveInput.y
                && _moveInput.y <= _userInputDataAsset.InputDeadZoon)
            {
                _moveInput.y = 0f;
            }
            return _moveInput;
        }
    }

    /// <summary>
    /// 攻撃判定
    /// </summary>
    public bool IsNormalAttack
    {
        get
        {
            return _isNormalAttack;
        }
    }

    public bool IsRoleAttack
    {
        get
        {
            return _isRoleAttack;
        }
    }

    public bool IsResurrection
    {
        get
        {
            return _isResurrection;
        }
    }

    public bool IsHoldTrigger
    {
        get
        {
            return _isHoldTrigger;
        }
    }

    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.actions[_userInputDataAsset.MoveActionName].performed += OnMove;
        _playerInput.actions[_userInputDataAsset.MoveActionName].canceled += OnMove;

        _playerInput.actions[_userInputDataAsset.NormalAttackName].started += OnNormalAttackDown;
        _playerInput.actions[_userInputDataAsset.NormalAttackName].canceled += OnNormalAttackUp;

        _playerInput.actions[_userInputDataAsset.RoleAttackName].started += OnNormalAttackDown;
        _playerInput.actions[_userInputDataAsset.RoleAttackName].canceled += OnNormalAttackUp;

        _playerInput.actions[_userInputDataAsset.ResurrectionName].started += OnResurrectionDown;
        _playerInput.actions[_userInputDataAsset.ResurrectionName].canceled += OnResurrectionUp;

        _playerInput.actions[_userInputDataAsset.HoldTriggerName].started += OnHoldTriggerDown;
        _playerInput.actions[_userInputDataAsset.HoldTriggerName].canceled += OnHoldTriggerUp;
    }

    /// <summary>
    /// 移動入力されたときに呼ばれる処理
    /// </summary>
    /// <param name="context">入力情報</param>
    private void OnMove(InputAction.CallbackContext context)
    {
        // 移動の入力取得
        _moveInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 通常攻撃の入力が押された時に呼ばれる処理
    /// </summary>
    /// <param name="context">入力情報</param>
    private void OnNormalAttackDown(InputAction.CallbackContext context)
    {
        // 通常攻撃判定を取得
        _isNormalAttack = context.ReadValueAsButton();
    }

    /// <summary>
    /// 通常攻撃の入力が離された時に呼ばれる処理
    /// </summary>
    /// <param name="context">入力情報</param>
    private void OnNormalAttackUp(InputAction.CallbackContext context)
    {
        _isNormalAttack = false;
    }

    /// <summary>
    /// ロール攻撃の入力が押された時に呼ばれる処理
    /// </summary>
    /// <param name="context">入力情報</param>
    private void OnRoleAttackDown(InputAction.CallbackContext context)
    {
        _isRoleAttack = context.ReadValueAsButton();
    }

    private void OnRoleAttackUp(InputAction.CallbackContext context)
    {
        _isRoleAttack = false;
    }

    /// <summary>
    /// 蘇生の入力が押された時に呼ばれる処理
    /// </summary>
    /// <param name="context">入力情報</param>
    private void OnResurrectionDown(InputAction.CallbackContext context)
    {
        _isResurrection = context.ReadValueAsButton();
    }

    /// <summary>
    /// 蘇生の入力が離された時に呼ばれる処理
    /// </summary>
    /// <param name="context">入力情報</param>
    private void OnResurrectionUp(InputAction.CallbackContext context)
    {
        _isResurrection = false;
    }

    /// <summary>
    /// 掴み/投げの入力が押された時に呼ばれる処理
    /// </summary>
    /// <param name="context">入力情報</param>
    private void OnHoldTriggerDown(InputAction.CallbackContext context)
    {
        _isHoldTrigger = context.ReadValueAsButton();
    }

    /// <summary>
    /// 掴み/投げの入力が離された時に呼ばれる処理
    /// </summary>
    /// <param name="context">入力情報</param>
    private void OnHoldTriggerUp(InputAction.CallbackContext context)
    {
        _isHoldTrigger = false;
    }
}