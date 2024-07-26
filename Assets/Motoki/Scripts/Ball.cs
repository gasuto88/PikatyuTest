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
	private float _ballSpeed = 0f;

	private float _damage = 0f;	

	private Transform _myTransform = default;

	private Transform _targetTransform = default;

	private BallMove _ballMove = default;
	
	private ElectronicShocksPool _ballPool = default;

	private CollisionManager _collisionManager = default;

	private Character _shotCharacter = default;

	#endregion

	/// <summary>
	/// 更新前処理
	/// </summary>
	private void Awake () 
	{
		_ballMove = GetComponent<BallMove>();

		// Script取得
		GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
		_ballPool = gameManager.GetComponent<ElectronicShocksPool>();
		_collisionManager = gameManager.GetComponent<CollisionManager>();
	}

	/// <summary>
	/// 更新処理
	/// </summary>
	private void Update()
	{
		_ballMove.MoveBall(_targetTransform.position,_ballSpeed);

		// 衝突した敵を取得
		Transform targetCharacter = _collisionManager.CollisionTarget(
			_myTransform.position, _myTransform.localScale, _myTransform.rotation, LAYER_ENEMY);

		// 衝突したら弾をしまう
		if (targetCharacter != null)
		{
			_ballPool.Close(this);
		}
	}

	public void SetParameter(Character shotCharacter, Transform targetTrasform,float damage)
	{
		_shotCharacter = shotCharacter;
		_targetTransform = targetTrasform;
		_damage = damage;		
	}
}