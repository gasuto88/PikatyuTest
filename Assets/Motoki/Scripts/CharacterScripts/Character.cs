/*-------------------------------------------------
* Character.cs
* 
* 作成日　2024/06/27
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

/// <summary>
/// キャラクタークラス
/// </summary>
[System.Serializable]
public class Character : MonoBehaviour
{
    // レイヤーの名前
    protected const string LAYER_ENEMY = "Enemy";

    #region フィールド変数

    [SerializeField, Header("プレイヤーデータ")]
    protected PlayerDataAsset _playerDataAsset = default;

    // 通常攻撃時間
    protected float _normalAttackTime = 0f;

    // 通常攻撃クールタイム
    protected float _normalAttackCoolTime = 0f;

    // ロール時間
    protected float _roleTime = 0f;

    // ロールクールタイム
    protected float _roleCoolTime = 0f;

    // 移動方向
    protected Vector3 _moveDirection = default;

    protected Vector3 _roleAttackDirection = default;

    // プレイヤーのQuaternion
    protected Quaternion _playerQuaternion = default;

    // プレイヤー入力クラス
    protected UserInput _userInput = default;

    // 移動計算クラス
    protected MoveCalculator _moveCalculator = default;

    protected CollisionManager _collisionManager = default;

    protected Rigidbody _rigidbody = default;

    // 自身のTransform
    protected Transform _myTransform = default;

    protected Transform _targetTransfrom = default;

    // 行動状態
    protected ActionState _actionState = default;

    // 生存状態
    protected AliveState _aliveState = default;

    protected AttackProcess _normalAttackState = default;

    protected AttackProcess _roleState = default;

    protected RoleButtonState _roleButtonState = default;

    protected ISearch _iSearch = default;

    protected bool _isNormalAttack = false;

    protected bool _isRoleAttack = false;

    protected float _longPressTime = 0f;

    #endregion

    #region プロパティ

    public Quaternion PlayerQuaternion { get => _playerQuaternion; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    protected void Start()
    {
        // 自身のTransformを設定
        _myTransform = transform;

        // 自身の回転角度を設定
        _playerQuaternion = _myTransform.rotation;

        // Script取得
        _userInput = GetComponent<UserInput>();
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        _collisionManager = gameManager.GetComponent<CollisionManager>();
        _iSearch = gameManager.GetComponent<ISearch>();

        _moveCalculator = new();

        _rigidbody = GetComponent<Rigidbody>();

        Init();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected virtual void Init() { }


    private void Update()
    {
        UpdateCharacter();
    }

    /// <summary>
    /// キャラクター更新処理
    /// </summary>
    protected virtual void UpdateCharacter()
    {
        // 移動の入力を取得
        Vector2 moveInput = _userInput.MoveInput;

        ActionStateMachine(moveInput);

        switch (_actionState)
        {
            // 待機
            case ActionState.IDLE:
                {
                    break;
                }
            // 移動
            case ActionState.MOVE:
                {
                    Move(moveInput);

                    break;
                }
            // 通常攻撃
            case ActionState.NORMAL_ATTACK:
                {
                    NormalAttack();

                    break;
                }
            // ロール攻撃
            case ActionState.ROLE_ATTACK:
                {
                    RoleAttack();

                    break;
                }
            // 蘇生
            case ActionState.RESURRECTION:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }

        // 回転を計算
        _playerQuaternion = _moveCalculator.CalculationRotate(_myTransform.rotation, _moveDirection, _playerDataAsset.RotationSpeed);

        // 回転角度を設定
        _myTransform.rotation = _playerQuaternion;

        // 移動を設定
        //_myTransform.position = _playerPosition;
    }

    /// <summary>
    /// 行動状態管理処理
    /// </summary>
    /// <param name="moveInput">移動入力</param>
    private void ActionStateMachine(Vector2 moveInput)
    {
        ChangeRoleAttackDirection();

        switch (_actionState)
        {
            case ActionState.IDLE:
                {
                    // 移動入力があったら移動状態にする
                    if (moveInput != Vector2.zero)
                    {
                        _actionState = ActionState.MOVE;
                    }
                    else if (_userInput.IsNormalAttack
                        && !_isNormalAttack)
                    {
                        _actionState = ActionState.NORMAL_ATTACK;
                    }
                    else if (_userInput.IsRoleAttack
                        && _roleButtonState == RoleButtonState.IDLE
                        && !_isRoleAttack)
                    {
                        _roleButtonState = RoleButtonState.SHORT;
                    }
                    break;
                }
            case ActionState.MOVE:
                {
                    if (_userInput.IsNormalAttack
                        && !_isNormalAttack)
                    {
                        _actionState = ActionState.NORMAL_ATTACK;
                    }
                    else if (_userInput.IsRoleAttack
                        && _roleButtonState == RoleButtonState.IDLE
                        && !_isRoleAttack)
                    {
                        _roleButtonState = RoleButtonState.SHORT;
                    }
                    // 移動入力がなかったら
                    else if (moveInput == Vector2.zero)
                    {
                        _actionState = ActionState.IDLE;
                    }

                    break;
                }
            case ActionState.NORMAL_ATTACK:
                {
                    break;
                }
            case ActionState.ROLE_ATTACK:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="moveInput">移動入力</param>
    private void Move(Vector2 moveInput)
    {
        // 移動方向を計算
        _moveDirection = Vector3.forward * moveInput.y + Vector3.right * moveInput.x;

        // 移動量を加算
        _rigidbody.velocity = _moveCalculator.CalculationMove(_moveDirection, _playerDataAsset.MoveSpeed);
    }

    /// <summary>
    /// 通常攻撃処理
    /// </summary>
    protected virtual void NormalAttack()
    {

    }
    /// <summary>
    /// ロール攻撃処理
    /// </summary>
    protected virtual void RoleAttack()
    {

    }

    protected virtual void ChangeRoleAttackDirection()
    {
        
    }

}
