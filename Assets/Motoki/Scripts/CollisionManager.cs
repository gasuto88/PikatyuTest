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
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class CollisionManager : MonoBehaviour
{

    public Transform CollisionTarget(Vector3 center, Vector3 halfSize, Quaternion myQuaternion, string[] layerNames, Transform myTransform)
    {
        // 判定にいる全部のオブジェクトを取得
        Collider[] targetColliders = Physics.OverlapBox(center, halfSize, myQuaternion, LayerMask.GetMask(layerNames));

        // オブジェクトがなかったら
        if (targetColliders.Length <= 0)
        {
            return null;
        }

        List<Transform> targetTransforms = new List<Transform>();

        // ぶつかる対象をまとめる
        foreach (Collider targetCollider in targetColliders)
        {
            // ぶつかる対象だったらListに追加
            if (targetCollider.transform != myTransform)
            {
                targetTransforms.Add(targetCollider.transform);
            }
        }
        // 自分に一番近い対象を取得
        Transform targetTransform = targetTransforms.OrderBy(obj => Vector3.Distance(obj.transform.position, center)).FirstOrDefault();

        return targetTransform;
    }

    /// <summary>
    /// 一番近い かつ つかめるものを返す
    /// </summary>
    /// <param name="center">中心座標</param>
    /// <param name="halfSize">当たり判定の大きさ</param>
    /// <param name="myQuaternion">自分の回転角度</param>
    /// <param name="layerNames">レイヤーの名前</param>
    /// /// <param name="myTransform">自分のTransform</param>
    /// <returns>つかめるもの</returns>
    public Transform SerachHoldObject(Vector3 center, Vector3 halfSize, Quaternion myQuaternion, string[] layerNames, Transform myTransform)
    {
        // ホールド判定にいる全部のオブジェクトを取得
        Collider[] targetColliders = Physics.OverlapBox(center, halfSize, myQuaternion, LayerMask.GetMask(layerNames));

        // オブジェクトがなかったら
        if (targetColliders.Length <= 0)
        {
            return null;
        }

        List<Transform> holdTransforms = new List<Transform>();

        // つかめるものをまとめる
        foreach (Collider targetCollider in targetColliders)
        {
            Debug.Log(targetCollider.name);
            if (targetCollider.TryGetComponent<IHoldable>(out IHoldable iholdable))
            {
                // つかめるものだったらListに追加
                if (iholdable.CanHold()
                    && targetCollider.transform != myTransform)
                {
                    holdTransforms.Add(targetCollider.transform);
                }
            }
        }
        // 自分に一番近いオブジェクトを取得
        Transform holdTransform = holdTransforms.OrderBy(obj => Vector3.Distance(obj.transform.position, center)).FirstOrDefault();

        return holdTransform;
    }

    /// <summary>
    /// バースト内にいる敵を取得する処理
    /// </summary>
    /// <param name="center">中心座標</param>
    /// <param name="radius">半径</param>
    /// <param name="layerName">レイヤーの名前</param>
    /// <returns></returns>
    public Collider[] TargetInBurst(Vector3 center, float radius, string layerName)
    {
        Collider[] targetColliders = Physics.OverlapSphere(center, radius, LayerMask.GetMask(layerName));

        return targetColliders;
    }
}