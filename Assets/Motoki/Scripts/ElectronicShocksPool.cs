/*-------------------------------------------------
* BallPool.cs
* 
* 作成日　2024/06/25
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 電気ショックプールクラス
/// </summary>
public class ElectronicShocksPool : MonoBehaviour 
{
	#region フィールド変数

	[SerializeField,Header("生成する弾")]
	private BallMove _createBall = default;

	[SerializeField,Header("生成数")]
	private uint _createCount = 0;

	private Queue<BallMove> _ballMoves = new Queue<BallMove>();

	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Start () 
	{
        for (int i = 0; i < _createCount; i++)
        {
			InstanceBall();
        }
	}

	/// <summary>
	/// 弾を生成する処理
	/// </summary>
	private void InstanceBall()
    {
		BallMove tempBall = Instantiate(_createBall);

		// 弾を不可視化
		tempBall.gameObject.SetActive(false);

		// 弾を格納
		_ballMoves.Enqueue(tempBall);
	}

	/// <summary>
	/// 取り出す
	/// </summary>
	/// <param name="createTransform">生成するTransform</param>
	public BallMove TakeOut(Vector3 myPosition,Quaternion myRotation)
    {
		// キューに弾がなかったら補充する
		if(_ballMoves.Count <= 0)
        {
			InstanceBall();
		}

		// キューから弾を取り出す
		BallMove tempBall = _ballMoves.Dequeue();

		// 弾を可視化
		tempBall.gameObject.SetActive(true);

		// 座標を設定
		tempBall.transform.position = myPosition;

		// 回転を設定
		tempBall.transform.rotation = myRotation;

		return tempBall;
    }

	/// <summary>
	/// 弾をしまう
	/// </summary>
	/// <param name="closeBall">しまう弾</param>
	public void Close(BallMove closeBall)
    {
		closeBall.gameObject.SetActive(false);

		_ballMoves.Enqueue(closeBall);
    }
}