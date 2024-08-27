/*-------------------------------------------------
* Ball.cs
* 
* 作成日　2024/07/26
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	#region 定数

	// レイヤーの名前
	protected const string LAYER_ENEMY = "Enemy";

	#endregion

	#region フィールド変数

	// 弾の速度
	protected float _ballSpeed = 0f;

	protected float _damage = 0f;

	protected Transform _myTransform = default;

	protected Transform _targetTransform = default;

	protected BallMove _ballMove = default;

	protected CollisionManager _collisionManager = default;

	protected Character _shotCharacter = default;

	protected BallPool _ballPool = default;

	#endregion

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Awake () 
	{
		_myTransform = transform;

		// Script取得
		_ballMove = GetComponent<BallMove>();
		GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");	
		_collisionManager = gameManager.GetComponent<CollisionManager>();

		Init();
	}

	/// <summary>
	/// 初期化処理
	/// </summary>
	protected virtual void Init()
    {

    }

	/// <summary>
	/// 更新処理
	/// </summary>
	private void Update()
	{
		UpdateBall();
	}

	/// <summary>
	/// 弾更新処理
	/// </summary>
	protected virtual void UpdateBall()
    {
		_ballMove.MoveElectronicShocksBall(_targetTransform.position, _ballSpeed);

		// 衝突した敵を取得
		Transform targetCharacter = _collisionManager.CollisionTarget(
			_myTransform.position, _myTransform.localScale, _myTransform.rotation, LAYER_ENEMY);

		// 衝突したら弾をしまう
		if (targetCharacter != null)
		{
			_ballPool.Close(this);
		}
	}

	public void SetParameter(Character shotCharacter, Transform targetTrasform,float damage,float ballSpeed)
	{
		_shotCharacter = shotCharacter;
		
		_damage = damage;
		_ballSpeed = ballSpeed;
		if (targetTrasform != null)
		{
			_targetTransform = targetTrasform;
			_ballMove.RotationBall(_targetTransform.position);
		}
	}
}