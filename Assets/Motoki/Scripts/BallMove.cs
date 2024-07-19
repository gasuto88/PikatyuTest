/*-------------------------------------------------
* BallMove.cs
* 
* 作成日　2024/06/25
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour 
{
	#region 定数

	// レイヤーの名前
	protected const string LAYER_ENEMY = "Enemy";

	#endregion

	#region フィールド変数

	// 弾の速度
	private float _ballSpeed = 0f;

	private Transform _targetTransform = default;

	private ElectronicShocksPool _ballPool = default;

	private CollisionManager _collisionManager = default;

	private Transform _myTransform = default;

	private Character _shotCharacter = default;

    #endregion

    #region プロパティ

	public float BallSpeed { get => _ballSpeed; set => _ballSpeed = value; }

	public Transform TargetTransform { get => _targetTransform; set => _targetTransform = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Awake () 
	{
		_myTransform = transform;

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
		MoveBall();
    }

	/// <summary>
	/// 弾の移動処理
	/// </summary>
    public void MoveBall()
    {
		// 敵の方向を計算
		Vector3 targetDirection = _targetTransform.position - _myTransform.position;

		_myTransform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);

		// 敵に向かって移動
		_myTransform.position += _myTransform.forward * _ballSpeed * Time.deltaTime;

		// 衝突した敵を取得
		Transform targetCharacter = _collisionManager.CollisionTarget(
			_myTransform.position, _myTransform.localScale, _myTransform.rotation, LAYER_ENEMY);		

		// 衝突したら弾をしまう
		if (targetCharacter != null)
        {	

			_ballPool.Close(this);
        }

    }

	public void SetCharacter(Character shotCharacter)
	{
		_shotCharacter = shotCharacter;
	}
}