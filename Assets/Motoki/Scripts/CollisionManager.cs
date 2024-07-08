/*-------------------------------------------------
* CollisionManager.cs
* 
* 作成日　2024/06/27
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollisionManager : MonoBehaviour 
{

	public Transform CollisionTarget(Vector3 center,Vector3 halfSize,Quaternion myQuaternion,string layerName)
    {
		Collider[] targetColliders = Physics.OverlapBox(center,halfSize, myQuaternion,LayerMask.GetMask(layerName));

		if(1 == targetColliders.Length)
        {		
			return targetColliders[0].transform;
        }
		else if(2 <= targetColliders.Length)
        {
			// 一番近いColliderを取得
			Collider nearestCollider
				= targetColliders.OrderBy(obj => Vector3.Distance(obj.transform.position, center)).FirstOrDefault();

			return nearestCollider.transform;
		}

		return null;
    }
}