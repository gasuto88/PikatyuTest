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
public class Character : MonoBehaviour, IHoldable, IHoldChange, IShotChange, ISetRigidbody
{
    // レイヤーの名前
    protected const string LAYER_PLAYER = "Player";
    protected const string LAYER_ENEMY = "Enemy";
    protected const string LAYER_ITEM = "Item";

    #region フィールド変数

    [SerializeField, Header("プレイヤーデータ")]
    protected PlayerDataAsset _playerData = default;

    // ホールドの当たり判定
    protected Vector3 _holdsize = default;

    // 通常攻撃時間
    protected float _normalAttackTime = 0f;

    // 通常攻撃クールタイム
    protected float _normalAttackCoolTime = 0f;

    // ロール時間
    protected float _roleTime = 0f;

    // ロールクールタイム
    protected float _roleCoolTime = 0f;

    // 回転時間
    protected float _rotationTime = 0f;

    // 移動方向
    protected Vector3 _moveDirection = default;

    protected Vector3 _roleAttackDirection = default;

    protected Vector3 _attackDirection = default;

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

    protected float _holdCoolTime = 0f;

    protected bool _isHold = false;

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

        _holdCoolTime = _playerData.HoldCoolTime;

        _rotationTime = _playerData.RotationTime;

        // ホールドの半分の当たり判定を設定
        _holdsize = new Vector3(_myTransform.localScale.x, _myTransform.localScale.y, _playerData.HoldDistance) / 2;

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

        Vector2 attackDirectionInput = _userInput.RightStickInput;

        // ホールド入力を取得
        bool isHoldInput = _userInput.IsRightTrigger;

        ActionStateMachine(moveInput);

        Hold(isHoldInput, attackDirectionInput);

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

                    if (_moveDirection != Vector3.zero
                        && _holdState != HoldState.TRIGGER)
                    {
                        // 回転を計算
                        CharacterRotate(_moveDirection);
                    }

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
                    Shot();

                    Transform collisionTarget
                        = _collisionManager.CollisionTarget(_myTransform.position, _myTransform.localScale
                        , _myTransform.rotation, _playerData.ColisionObjects, _myTransform);

                    if (collisionTarget != null)
                    {
                        switch (collisionTarget.tag)
                        {
                            case LAYER_PLAYER:
                                {                                  
                                    Collider[] targetColliders 
                                        = _collisionManager.TargetInBurst(_myTransform.position, _playerData.BurstRadius,LAYER_ENEMY);

                                    //// バースト範囲内にいる全ての敵にダメージを与える
                                    //foreach (Collider targetCollider in targetColliders)
                                    //{
                                    //    if(targetCollider.TryGetComponent<IDamageable>(out IDamageable damageable))
                                    //    {
                                    //        damageable.Damage(_playerData.BurstDamage);
                                    //    }
                                    //}

                                    break;
                                }
                            case LAYER_ENEMY:
                                {
                                    //    if(targetCollider.TryGetComponent<IDamageable>(out IDamageable damageable))
                                    //    {
                                    //        damageable.Damage(_playerData.BurstDamage);
                                    //    }
                                    break;
                                }
                            default:
                                {
                                    
                                    break;
                                }
                        }
                        _actionState = ActionState.IDLE;
                    }

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
        Quaternion playerRotate = _moveCalculator.CalculationRotate(_myTransform.rotation, rotateDirection, _playerData.RotationSpeed);
        //Quaternion playerRotate = Quaternion.LookRotation(rotateDirection, Vector3.up);
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
    private void Hold(bool isHoldInput, Vector2 attackDirectionInput)
    {
        switch (_holdState)
        {
            case HoldState.IDLE:
                {
                    if (isHoldInput
                        && (_actionState == ActionState.IDLE || _actionState == ActionState.MOVE))
                    {
                        Vector3 holdDirection = Vector3.forward * attackDirectionInput.y + Vector3.right * attackDirectionInput.x;

                        Vector3 holdPosition = _myTransform.position + holdDirection * (_holdsize.z / 2);

                        Quaternion holdRotation;

                        if (holdDirection == Vector3.zero)
                        {
                            holdRotation = _myTransform.rotation;
                        }
                        else
                        {
                            holdRotation = Quaternion.LookRotation(holdDirection, Vector3.up);
                        }
                        // つかめるものがないかチェック
                        _holdTarget = _collisionManager.SerachHoldObject(
                            holdPosition, _holdsize, holdRotation, _playerData.HoldObjects, _myTransform);

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

                                    _holdTarget.transform.rotation = _myTransform.rotation;

                                    if (_holdTarget.TryGetComponent<IHoldChange>(out IHoldChange iholdChange))
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
                    // 攻撃方向を取得
                    _attackDirection = Vector3.forward * attackDirectionInput.y + Vector3.right * attackDirectionInput.x;

                    // 入力がなかったらプレイヤーの向いてる方向をむく
                    if (_attackDirection == Vector3.zero)
                    {
                        _attackDirection = _myTransform.forward;
                    }

                    _holdCoolTime -= Time.deltaTime;

                    if (isHoldInput
                        && _holdCoolTime <= 0f)
                    {
                        _holdCoolTime = _playerData.HoldCoolTime;

                        _holdState = HoldState.TRIGGER;
                    }

                    break;
                }
            case HoldState.TRIGGER:
                {
                    CharacterRotate(_attackDirection);

                    _rotationTime -= Time.deltaTime;

                    // プレイヤーの回転が終わったら投げる
                    if (_rotationTime <= 0f)
                    {
                        _rotationTime = _playerData.RotationTime;

                        _holdTarget.parent = null;

                        if (_holdTarget.TryGetComponent<ISetRigidbody>(out ISetRigidbody setRigidbody))
                        {
                            setRigidbody.SetRigidbody();
                        }

                        if (_holdTarget.TryGetComponent<IShotChange>(out IShotChange shotChange))
                        {
                            shotChange.ChangeShotState();
                        }

                        _holdState = HoldState.IDLE;
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    protected void Shot()
    {
        _myRigidbody.velocity = _myTransform.forward * _playerData.ShotSpeed;
    }

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

    public void ChangeShotState()
    {
        _actionState = ActionState.SHOT;
    }

    public void SetRigidbody()
    {
        gameObject.AddComponent<Rigidbody>();

        _myRigidbody = GetComponent<Rigidbody>();

        _myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

}
