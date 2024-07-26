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

    [SerializeField, Header("通常攻撃方法")]
    protected AttackNormalType _normalAttackType = default;

    // 通常攻撃時間
    protected float _normalAttackTime = 0f;

    // 通常攻撃クールタイム
    protected float _normalAttackCoolTime = 0f;

    // プレイヤー座標
    protected Vector3 _playerPosition = default;

    // 移動方向
    protected Vector3 _moveDirection = default;

    protected Vector3 _roleAttackDirection = default;

    // プレイヤーのQuaternion
    protected Quaternion _playerQuaternion = default;

    // プレイヤー入力クラス
    protected UserInput _userInput = default;

    // 移動計算クラス
    protected MoveCalculator _moveCalculator = default;

    // 自身のTransform
    protected Transform _myTransform = default;

    // 行動状態
    protected ActionState _actionState = default;

    // 生存状態
    protected AliveState _aliveState = default;

    protected AttackProcess _attackState = default;

    protected RoleAttackProcess _roleAttackProcess = default;

    protected INormalAttack _iNormalAttack = default;

    protected IRoleAttack _iRoleAttack = default;

    protected ISearch _iSearch = default;

    protected NormalAttackDataAsset _normalAttackData = default;

    protected RoleDataAsset _roleAttackData = default;

    protected AttackDataManager _skillManager = default;

    protected bool _isNormalAttack = false;

    protected bool _isRoleAttack = false;

    protected float _longPressTime = 0f;

    #endregion

    #region プロパティ

    public Vector3 PlayerPosition { get => _playerPosition; }

    public Quaternion PlayerQuaternion { get => _playerQuaternion; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    protected void Start()
    {
        // 自身のTransformを設定
        _myTransform = transform;

        // 自身の座標を設定
        _playerPosition = _myTransform.position;

        // 自身の回転角度を設定
        _playerQuaternion = _myTransform.rotation;

        // Script取得
        _userInput = GetComponent<UserInput>();
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        _skillManager = gameManager.GetComponent<AttackDataManager>();
        _iSearch = gameManager.GetComponent<ISearch>();

        _moveCalculator = new();

        // 通常攻撃のインターフェイスを取得
        _iNormalAttack = NormalAttackEnum._normalAttackEnum.Values.ToArray()[(int)_normalAttackType];        

        // 通常攻撃データを取得
        _normalAttackData = _skillManager.ReturnNormalAttackData((int)_normalAttackType);

        // 通常攻撃のダメージを設定する
        _iNormalAttack.SetDamage(_normalAttackData.NormalAttackDamage);

        // 通常攻撃時間を設定
        _normalAttackTime = _normalAttackData.NormalAttackTime;

        // 通常攻撃クールタイムを設定
        _normalAttackCoolTime = _normalAttackData.NormalAttackCoolTime;

        _longPressTime = _playerDataAsset.LongPressTime;
      
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
    /// キャラクターを更新する処理
    /// </summary>
    private void UpdateCharacter()
    {
        // 移動の入力を取得
        Vector2 moveInput = _userInput.MoveInput;

        // 敵を取得
        Transform targetTransform = _iSearch.TargetSearch(_playerPosition, _normalAttackData.NormalAttackDistance, LAYER_ENEMY);

        ActionStateMachine(moveInput, targetTransform);

        if (_isNormalAttack)
        {
            _normalAttackCoolTime -= Time.deltaTime;

            if(_normalAttackCoolTime <= 0f)
            {
                _normalAttackCoolTime = _normalAttackData.NormalAttackCoolTime;

                _isNormalAttack = false;
            }
        }

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
                    NormalAttack(targetTransform);

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
        _myTransform.position = _playerPosition;
    }

    /// <summary>
    /// 行動状態管理処理
    /// </summary>
    /// <param name="moveInput">移動入力</param>
    private void ActionStateMachine(Vector2 moveInput,Transform targetTransform)
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
                        && targetTransform != null
                        && !_isNormalAttack)
                    {
                        _actionState = ActionState.NORMAL_ATTACK;
                    }
                    else if (_userInput.IsRoleAttack
                        && _roleAttackProcess != RoleAttackProcess.IDLE)
                    {
                        _roleAttackProcess = RoleAttackProcess.SHORT;
                    }
                    break;
                }
            case ActionState.MOVE:
                {
                    if (_userInput.IsNormalAttack
                        && targetTransform != null
                        && !_isNormalAttack)
                    {
                        _actionState = ActionState.NORMAL_ATTACK;
                    }
                    else if (_userInput.IsRoleAttack)
                    {
                        _roleAttackProcess = RoleAttackProcess.SHORT;
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
        _playerPosition += _moveCalculator.CalculationMove(_moveDirection, _playerDataAsset.MoveSpeed);
    }

    /// <summary>
    /// 通常攻撃処理
    /// </summary>
    private void NormalAttack(Transform targetTransform)
    {
        switch (_attackState)
        {
            // 初期化
            case AttackProcess.INIT:
                {
                    _isNormalAttack = true;
                    // 敵の方向を取得
                    _moveDirection = targetTransform.position - _playerPosition;                  

                    _iNormalAttack.Init(_playerPosition, _playerQuaternion,this,targetTransform);

                    _attackState = AttackProcess.EXECUTE;

                    break;
                }
            // 実行
            case AttackProcess.EXECUTE:
                {
                    _iNormalAttack.Execute(_playerPosition, _playerQuaternion);

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
                    _iNormalAttack.Exit(_playerPosition, _playerQuaternion);

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
    
    private void RoleAttack()
    {
        _moveDirection = _roleAttackDirection;     

        //_iRoleAttack.Init(_playerPosition,_playerQuaternion,this);
    }

    private void ChangeRoleAttackDirection()
    {
        switch (_roleAttackProcess)
        {
            case RoleAttackProcess.SHORT:
                {
                    _roleAttackDirection = _myTransform.forward;

                    if (!_userInput.IsRoleAttack)
                    {
                        _actionState = ActionState.ROLE_ATTACK;
                    }

                    _longPressTime -= Time.deltaTime;

                    if (_longPressTime <= 0f)
                    {
                        _longPressTime = _playerDataAsset.LongPressTime;

                        _roleAttackProcess = RoleAttackProcess.LONG;
                    }

                    break;
                }
            case RoleAttackProcess.LONG:
                {
                    Vector3 attackDirectionInput = _userInput.AttackDirectionInput;

                    _roleAttackDirection 
                        = Vector3.forward * attackDirectionInput.y + Vector3.right * attackDirectionInput.x;

                    if (_userInput.IsCancel)
                    {
                        _roleAttackProcess = RoleAttackProcess.IDLE;
                    }
                    if (!_userInput.IsRoleAttack)
                    {
                        _actionState = ActionState.ROLE_ATTACK;
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
