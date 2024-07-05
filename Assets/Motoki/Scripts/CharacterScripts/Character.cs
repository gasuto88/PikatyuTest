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

[System.Serializable]
public class Character : MonoBehaviour
{
    // レイヤーの名前
    protected const string LAYER_ENEMY = "Enemy";

    #region フィールド変数

    [SerializeField,Header("通常攻撃方法")]
    protected NormalAttackType _normalAttackType = default;

    [SerializeField, Header("通常攻撃時間"), Min(0f)]
    protected float _normalAttackTime = 0f;

    [SerializeField, Header("通常攻撃距離"),Min(0f)]
    protected float _normalAttackDistance = 0f;

    [SerializeField, Header("ロール攻撃方法")]
    protected RoleAttackType _roleAttackType = default;

    [SerializeField, Header("移動速度"), Min(0f)]
    protected float _moveSpeed = 0f;

    [SerializeField, Header("回転速度"), Min(0f)]
    protected float _rotationSpeed = 0f;

    // 通常攻撃時間（初期化用）
    protected float _initNormalAttackTime = 0f;

    // プレイヤー座標
    protected Vector3 _playerPosition = default;

    // 移動方向
    protected Vector3 _moveDirection = default;

    // プレイヤーのQuaternion
    protected Quaternion _playerQuaternion = default;

    // プレイヤー入力クラス
    protected UserInput _userInput = default;

    protected MoveCalculator _moveCalculator = default;

    // 自身のTransform
    protected Transform _myTransform = default;

    // キャラクターステータス
    protected CharacterStatus _characterStatus = default;

    // 行動状態
    protected ActionState _actionState = default;

    // 生存状態
    protected AliveState _aliveState = default;

    protected AttackState _attackState = default;

    protected INormalAttack _iNormalAttack = default;

    protected ISearch _iSearch = default;

    protected int _normalAttackNumber = 0;

    protected bool _isNomalAttack = false;

    #endregion

    #region プロパティ

    public Vector3 PlayerPosition { get => _playerPosition; }

    public Quaternion PlayerRotation { get => _playerQuaternion; }

    public int NormalAttackNumber { get => _normalAttackNumber; set => _normalAttackNumber = value; }

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

        _iSearch = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ISearch>();
        
        _moveCalculator = new();

        // 通常攻撃時間を設定
        _initNormalAttackTime = _normalAttackTime;

        _characterStatus._moveSpeed = _moveSpeed;
    }

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
        _playerQuaternion = _moveCalculator.CalculationRotate(_myTransform.rotation, _moveDirection, _rotationSpeed);

        // 回転角度を設定
        _myTransform.rotation = _playerQuaternion;

        // 移動を設定
        _myTransform.position = _playerPosition;
    }

    /// <summary>
    /// 行動状態管理処理
    /// </summary>
    /// <param name="moveInput">移動入力</param>
    private void ActionStateMachine(Vector2 moveInput)
    {
        switch (_actionState)
        {
            case ActionState.IDLE:
                {
                    if (_userInput.IsNormalAttack)
                    {                      
                        _actionState = ActionState.NORMAL_ATTACK;
                    }
                    else if (_userInput.IsRoleAttack)
                    {
                        _actionState = ActionState.ROLE_ATTACK;
                    }

                    // 移動入力があったら移動状態にする
                    else if (moveInput != Vector2.zero)
                    {
                        _actionState = ActionState.MOVE;
                    }
                    break;
                }
            case ActionState.MOVE:
                {
                    if (_userInput.IsNormalAttack)
                    {
                        _actionState = ActionState.NORMAL_ATTACK;
                    }
                    else if (_userInput.IsRoleAttack)
                    {
                        _actionState = ActionState.ROLE_ATTACK;
                    }

                    // 移動入力がなかったら
                    if (moveInput == Vector2.zero)
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
        _playerPosition += _moveCalculator.CalculationMove(_moveDirection, _characterStatus._moveSpeed);
    }

    /// <summary>
    /// 通常攻撃処理
    /// </summary>
    private void NormalAttack()
    {
        switch (_attackState)
        {
            // 初期化
            case AttackState.INIT:
                {
                    // 通常攻撃のインターフェイスを取得
                    _iNormalAttack = NormalAttackEnum._normalAttackEnum.Values.ToArray()[(int)_normalAttackType];

                    // 敵の方向取得して回転
                    Transform targetTransform = _iSearch.TargetSearch(_playerPosition,_normalAttackDistance,LAYER_ENEMY);

                    // 範囲内に敵がいたら
                    if (targetTransform != null)
                    {
                        // 敵の方向を取得
                        _moveDirection = targetTransform.position - _playerPosition;

                        // 敵の方向を向く
                        _playerQuaternion = Quaternion.LookRotation(_moveDirection, Vector3.up);
                    }
                    _iNormalAttack.Init(_playerPosition, _playerQuaternion);

                    _attackState = AttackState.EXECUTE;

                    break;
                }
            // 実行
            case AttackState.EXECUTE:
                {
                    _iNormalAttack.Execute(_playerPosition, _playerQuaternion);

                    _normalAttackTime -= Time.deltaTime;

                    if (_normalAttackTime <= 0f)
                    {
                        _normalAttackTime = _initNormalAttackTime;

                        _attackState = AttackState.EXIT;
                    }

                    break;
                }
            // 終了
            case AttackState.EXIT:
                {
                    _iNormalAttack.Exit(_playerPosition, _playerQuaternion);

                    _attackState = AttackState.INIT;

                    _actionState = ActionState.IDLE;
                    
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

}
