/*-------------------------------------------------
* SearchManager.cs
* 
* 作成日　2024/06/25
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchManager : MonoBehaviour, ISearch
{
	#region フィールド変数
	#endregion

	/// <summary>
    /// 更新前処理
    /// </summary>
	private void Start () 
	{
		
	}

	public Transform TargetSearch(Vector3 myPosition, float distance, string layer)
    {
		//範囲にいる対象を取得
		Collider[] targetColliders = Physics.OverlapSphere(myPosition, distance, LayerMask.GetMask(layer));

		if (1 == targetColliders.Length)
		{
			return targetColliders[0].transform;
		}
		else if (2 <= targetColliders.Length)
		{
			Collider nearestCollider
				= targetColliders.OrderBy(obj => Vector3.Distance(obj.transform.position, myPosition)).FirstOrDefault();

			return nearestCollider.transform;
		}

        

		return null;
    }
}