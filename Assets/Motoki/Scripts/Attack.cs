/*-------------------------------------------------
* Attack.cs
* 
* 作成日　2024/06/18
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃クラス
/// </summary>
public abstract class AttackClass : MonoBehaviour 
{
	#region フィールド変数

	protected BallPool _ballPool = default;

	protected Player _player = default;

	protected NormalAttackState _normalAttackState = NormalAttackState.IDLE;

	public enum NormalAttackState
	{
		IDLE,
		START,
		ATTACK,
		END
	}

    #endregion

    #region プロパティ

	public NormalAttackState NormalAttackType { get => _normalAttackState; set => _normalAttackState = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start () 
	{
		// Script取得
		_ballPool = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BallPool>();
		_player = GetComponent<Player>();
	}

	/// <summary>
	/// 通常攻撃処理
	/// </summary>
	public virtual void NormalAttack()
    {
		
    }

	/// <summary>
	/// ロール攻撃処理
	/// </summary>
	public virtual void RoleAttack()
    {

    }
}