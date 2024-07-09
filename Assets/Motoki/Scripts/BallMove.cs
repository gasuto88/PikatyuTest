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

	[SerializeField,Header("弾の速度kamo"),Min(0f)]
	private float _ballSpeed = 0f;

	private Transform _targetTransform = default;

	private BallPool _ballPool = default;

	private CollisionManager _collisionManager = default;

	private Transform _myTransform = default;

    #endregion

    #region プロパティ

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
		_ballPool = gameManager.GetComponent<BallPool>();
		_collisionManager = gameManager.GetComponent<CollisionManager>();
	}

    private void Update()
    {
		MoveBall();
    }

    public void MoveBall()
    {
		Vector3 targetDirection = _targetTransform.position - _myTransform.position;

		_myTransform.position += targetDirection * _ballSpeed * Time.deltaTime;

		Transform targetCharacter = _collisionManager.CollisionTarget(
			_myTransform.position, _myTransform.localScale, _myTransform.rotation, LAYER_ENEMY);		

		if (targetCharacter != null)
        {
			_ballPool.Close(this);
        }

    }
}