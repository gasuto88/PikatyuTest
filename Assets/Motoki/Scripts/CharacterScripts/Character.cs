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
using UnityEngine.EventSystems;
using Unity.VisualScripting;

/// <summary>
/// キャラクタークラス
/// </summary>
[System.Serializable]
public class Character : MonoBehaviour, IHoldable,IHoldChange
{
    // レイヤーの名前
    protected const string LAYER_PLAYER = "Player";
    protected const string LAYER_ENEMY = "Enemy";
    protected const string LAYER_ITEM = "Item";

    #region フィールド変数

    [SerializeField, Header("プレイヤーデータ")]
    protected PlayerDataAsset _playerData = default;

    [SerializeField,Header("ホールドの当たり判定")]
    protected Vector3 _holdsize = default;

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

    protected Rigidbody _myRigidbody = default;

    // 自身のTransform
    protected Transform _myTransform = default;

    protected Transform _targetTransfrom = default;

    protected Transform _holdTarget = default;

    // 行動状態
    protected ActionState _actionState = default;

    // 生存状態
    protected AliveState _aliveState = default;

    protected AttackProcess _normalAttackState = default;

    protected AttackProcess _roleState = default;

    protected RoleButtonState _roleButtonState = default;

    // 掴みステート
    protected HoldState _holdState = default;

    protected ISearch _iSearch = default;

    // 通常攻撃判定
    protected bool _isNormalAttack = false;

    // ロール攻撃判定
    protected bool _isRoleAttack = false;

    // ロールキャンセル判定
    protected bool _isRoleCancel = false;

    // ロール長押し時間
    protected float _roleLongPressTime = 0f;

    // ロールキャンセルクールタイム
    protected float _roleCancelCoolTime = 0f;

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

        _myRigidbody = GetComponent<Rigidbody>();

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
        Vector2 moveInput = _userInput.LeftStickInput;

        // ホールド入力を取得
        bool isHoldInput = _userInput.IsButtonNorth;

        ActionStateMachine(moveInput);

        Hold(isHoldInput);

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
            // つかまれ状態
            case ActionState.HELD:
                {
                    break;
                }
            // ふっ飛ばされ状態
            case ActionState.SHOT:
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

        if (_moveDirection != Vector3.zero)
        {
            // 回転を計算
            //_myTransform.rotation = _moveCalculator.CalculationRotate(_myTransform.rotation, _moveDirection, _playerData.RotationSpeed);
            CharacterRotate(_moveDirection);
        }
        // 回転角度を設定
        //_myTransform.rotation = _playerQuaternion;
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
                    else if (_userInput.IsButtonEast
                        && !_isNormalAttack)
                    {
                        _actionState = ActionState.NORMAL_ATTACK;
                    }
                    else if (_userInput.IsLeftTrigger
                        && _roleButtonState == RoleButtonState.IDLE
                        && !_isRoleAttack)
                    {
                        _roleButtonState = RoleButtonState.SHORT;
                    }
                    break;
                }
            case ActionState.MOVE:
                {
                    if (_userInput.IsButtonEast
                        && !_isNormalAttack)
                    {
                        _actionState = ActionState.NORMAL_ATTACK;
                    }
                    else if (_userInput.IsLeftTrigger
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
        _myRigidbody.velocity = _moveCalculator.CalculationMove(_moveDirection, _playerData.MoveSpeed);
    }

    protected void CharacterRotate(Vector3 rotateDirection)
    {
        Debug.Log("お回転");
        Quaternion playerRotate = _moveCalculator.CalculationRotate(_myTransform.rotation, rotateDirection, _playerData.RotationSpeed);
        _myRigidbody.MoveRotation(playerRotate);        
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

    /// <summary>
    /// ホールド処理
    /// </summary>
    /// <param name="isHoldInput"></param>
    private void Hold(bool isHoldInput)
    {
        switch (_holdState)
        {
            case HoldState.IDLE:
                {
                    if (isHoldInput
                        && (_actionState == ActionState.IDLE || _actionState == ActionState.MOVE))
                    {                                                            
                        // つかめるものがないかチェック
                        _holdTarget = _collisionManager.SerachHoldObject(
                            _myTransform.position,_holdsize , _myTransform.rotation, _playerData.HoldObjects, _myTransform);

                        Debug.Log(_holdTarget);
                        
                        if (_holdTarget == null)
                        {
                            return;
                        }
                        
                        switch (_holdTarget.tag)
                        {
                            // プレイヤー
                            case LAYER_PLAYER:
                                {
                                    Rigidbody targetRigidbody = _holdTarget.GetComponent<Rigidbody>();

                                    Destroy(targetRigidbody);

                                    // 親子
                                    _holdTarget.parent = _myTransform;

                                    _holdTarget.position = _myTransform.position + _myTransform.forward * 2;

                                    if(_holdTarget.TryGetComponent<IHoldChange>(out IHoldChange iholdChange))
                                    {
                                        iholdChange.ChangeHoldState();
                                    }

                                    break;
                                }
                            // 敵
                            case LAYER_ENEMY:
                                {
                                    break;
                                }
                            // アイテム
                            case LAYER_ITEM:
                                {
                                    break;
                                }
                            default:
                                {
                                    break;
                                }

                        }

                        _holdState = HoldState.HOLD;

                    }

                    break;
                }
            case HoldState.HOLD:
                {
                    if (_userInput.IsButtonSouth)
                    {
                        _holdState = HoldState.TRIGGER;
                    }

                    break;
                }
            case HoldState.TRIGGER:
                {
                    _holdTarget.AddComponent<Rigidbody>();

                    Rigidbody targetRigidbody = _holdTarget.GetComponent<Rigidbody>();

                    
                    targetRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(_myTransform.position , _holdsize);
    //}

    protected virtual void ChangeRoleAttackDirection()
    {

    }

    public bool CanHold()
    {
        return true;
    }

    public void ChangeHoldState()
    {
        _actionState = ActionState.HELD;
    }

}
