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

        _longPressTime = _playerDataAsset.LongPressTime;
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
    }

    protected override void NormalAttack()
    {
        switch (_attackState)
        {
            // 初期化
            case AttackProcess.INIT:
                {
                    _isNormalAttack = true;

                    // 敵を取得
                    Transform targetTransform
                        = _iSearch.TargetSearch(_playerPosition, _normalAttackData.NormalAttackDistance, LAYER_ENEMY);

                    // 敵が範囲に入ってたら
                    if (targetTransform == null)
                    {
                        _actionState = ActionState.IDLE;
                        return;
                    }

                    // 敵の方向を取得
                    _moveDirection = targetTransform.position - _playerPosition;

                    // 弾を取り出す
                    Ball ball = _electronicShocksPool.TakeOut(_playerPosition, _playerQuaternion);

                    // 弾にパラメータを設定
                    ball.SetParameter(this, targetTransform, _normalAttackData.BallDamage, _normalAttackData.BallSpeed);

                    _attackState = AttackProcess.EXECUTE;

                    break;
                }
            // 実行
            case AttackProcess.EXECUTE:
                {
                    _normalAttackTime -= Time.deltaTime;

                    if (_normalAttackTime <= 0f)
                    {
                        _normalAttackTime = _normalAttackData.NormalAttackTime;

                        _attackState = AttackProcess.EXIT;
                    }

                    break;
                }
            // 終了
            case AttackProcess.EXIT:
                {
                    _attackState = AttackProcess.INIT;

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
        _moveDirection = _roleAttackDirection;

        //_iRoleAttack.Init(_playerPosition,_playerQuaternion,this);
    }
}