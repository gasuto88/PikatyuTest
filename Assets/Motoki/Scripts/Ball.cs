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

	protected float _burstRadius = 0f;

	protected float _burstDamage = 0f;

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

	public void SetBurstInfo(float damage,float radius)
    {
		_burstDamage = damage;

		_burstRadius = radius;
    }
}