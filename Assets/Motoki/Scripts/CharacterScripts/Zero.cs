/*-------------------------------------------------
* Zero.cs
* 
* 作成日　2024/08/23
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zero : Attack
{
    #region フィールド変数

    [SerializeField, Header("通常攻撃データ")]
    private BallNormalAttackDataAsset _normalAttackData = default;

    [SerializeField, Header("ロールデータ")]
    private BallRoleDataAsset _roleData = default;

    private ElectricBallPool _electricBallPool = default;

    private ElectronicShocksPool _electronicShocksPool = default;

    #endregion

    protected override void Init()
    {
        // script取得
        GameObject ballPoolManager = GameObject.FindGameObjectWithTag("BallPool");
        _electricBallPool = ballPoolManager.GetComponent<ElectricBallPool>();
        _electronicShocksPool = ballPoolManager.GetComponent<ElectronicShocksPool>();

        // 通常攻撃時間を設定
        _normalAttackTime = _normalAttackData.NormalAttackTime;

        // 通常攻撃クールタイムを設定
        _normalAttackCoolTime = _normalAttackData.NormalAttackCoolTime;

        _roleTime = _roleData.RoleAttackTime;

        _roleCoolTime = _roleData.RoleAttackCoolTime;

        _roleLongPressTime = _roleData.LongPressTime;

        _roleCancelCoolTime = _roleData.CancelCoolTime;
    }

    protected override void UpdateCharacter()
    {
        base.UpdateCharacter();

        if (_isNormalAttack)
        {
            _normalAttackCoolTime -= Time.deltaTime;

            if (_normalAttackCoolTime <= 0f)
            {
                _normalAttackCoolTime = _normalAttackData.NormalAttackCoolTime;

                _isNormalAttack = false;
            }
        }

        if (_isRoleAttack)
        {
            _roleCoolTime -= Time.deltaTime;

            if (_roleCoolTime <= 0f)
            {
                _roleCoolTime = _roleData.RoleAttackCoolTime;

                _isRoleAttack = false;
            }
        }

        if (_isRoleCancel)
        {
            _roleCancelCoolTime -= Time.deltaTime;

            if (_roleCancelCoolTime <= 0f)
            {
                _roleCancelCoolTime = _roleData.CancelCoolTime;

                _isRoleCancel = false;
            }
        }
    }

    protected override void NormalAttack()
    {
        switch (_normalAttackState)
        {
            // 初期化
            case AttackProcess.INIT:
                {
                    _isNormalAttack = true;

                    // 敵を取得
                    _targetTransfrom
                        = _iSearch.TargetSearch(_myTransform.position, _normalAttackData.NormalAttackDistance, LAYER_ENEMY);

                    // 敵が範囲に入ってたら
                    if (_targetTransfrom == null)
                    {
                        _actionState = ActionState.IDLE;
                        return;
                    }

                    // 敵の方向を取得
                    _moveDirection = _targetTransfrom.position - _myTransform.position;

                    // 弾を取り出す
                    Ball ball = _electronicShocksPool.TakeOut(_myTransform.position, _playerQuaternion);

                    // 弾にパラメータを設定
                    ball.SetParameter(this, _targetTransfrom, _normalAttackData.BallDamage, _normalAttackData.BallSpeed);

                    _normalAttackState = AttackProcess.EXECUTE;

                    break;
                }
            // 実行
            case AttackProcess.EXECUTE:
                {
                    _normalAttackTime -= Time.deltaTime;

                    if (_normalAttackTime <= 0f)
                    {
                        _normalAttackTime = _normalAttackData.NormalAttackTime;

                        _normalAttackState = AttackProcess.EXIT;
                    }

                    break;
                }
            // 終了
            case AttackProcess.EXIT:
                {
                    _normalAttackState = AttackProcess.INIT;

                    _actionState = ActionState.IDLE;

                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    protected override void RoleAttack()
    {
        //Debug.Log("ロール攻撃");
        _moveDirection = _roleAttackDirection;

        if (_moveDirection != Vector3.zero)
        {
            _playerQuaternion = Quaternion.LookRotation(_moveDirection, Vector3.up);
        }
        switch (_roleState)
        {
            // 初期化
            case AttackProcess.INIT:
                {
                    _isRoleAttack = true;

                    // 弾を取り出す
                    Ball ball = _electricBallPool.TakeOut(_myTransform.position, _playerQuaternion);

                    // 弾にパラメータを設定
                    ball.SetParameter(this, _targetTransfrom, _roleData.BallDamage, _roleData.BallSpeed);

                    ball.SetBurstInfo(_roleData.BurstDamage, _roleData.BurstRadius);

                    _roleState = AttackProcess.EXECUTE;

                    break;
                }
            // 実行
            case AttackProcess.EXECUTE:
                {
                    _roleTime -= Time.deltaTime;

                    if (_roleTime <= 0f)
                    {
                        _roleTime = _roleData.RoleAttackTime;

                        _roleState = AttackProcess.EXIT;
                    }

                    break;
                }
            // 終了
            case AttackProcess.EXIT:
                {
                    _roleState = AttackProcess.INIT;

                    _actionState = ActionState.IDLE;

                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    protected override void ChangeRoleAttackDirection()
    {
        switch (_roleButtonState)
        {
            // 短押し
            case RoleButtonState.SHORT:
                {
                    if (_isRoleCancel)
                    {
                        _roleButtonState = RoleButtonState.IDLE;
                        return;
                    }
                    // 敵を取得
                    _targetTransfrom
                        = _iSearch.TargetSearch(_myTransform.position, _roleData.RoleAttackDistance, LAYER_ENEMY);

                    if (_targetTransfrom == null)
                    {
                        _roleAttackDirection = _myTransform.forward;
                    }
                    else
                    {
                        // 敵の方向を取得
                        _moveDirection = _targetTransfrom.position - _myTransform.position;
                    }

                    if (_userInput.IsButtonSouth)
                    {
                        _isRoleCancel = true;
                        _roleButtonState = RoleButtonState.IDLE;
                        _roleLongPressTime = _roleData.LongPressTime;
                        return;
                    }
                    else if (!_userInput.IsLeftTrigger)
                    {
                        _roleButtonState = RoleButtonState.IDLE;
                        _actionState = ActionState.ROLE_ATTACK;
                        _roleLongPressTime = _roleData.LongPressTime;
                        return;
                    }
                    _roleLongPressTime -= Time.deltaTime;

                    if (_roleLongPressTime <= 0f)
                    {
                        _roleLongPressTime = _roleData.LongPressTime;

                        _roleButtonState = RoleButtonState.LONG;
                    }

                    break;
                }
            // 長押し
            case RoleButtonState.LONG:
                {
                    Vector3 attackDirectionInput = _userInput.RightStickInput;

                    // ロール攻撃方向
                    _roleAttackDirection
                        = Vector3.forward * attackDirectionInput.y + Vector3.right * attackDirectionInput.x;

                    if (_userInput.IsButtonSouth)
                    {
                        _isRoleCancel = true;
                        _roleButtonState = RoleButtonState.IDLE;
                        _roleLongPressTime = _roleData.LongPressTime;
                    }
                    else if (!_userInput.IsLeftTrigger)
                    {
                        _roleButtonState = RoleButtonState.IDLE;
                        _actionState = ActionState.ROLE_ATTACK;
                        _roleLongPressTime = _roleData.LongPressTime;
                    }

                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}