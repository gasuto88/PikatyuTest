/*-------------------------------------------------
* 作成日　2024/06/27
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

public interface ISearch
{
    public Transform TargetSearch(Vector3 myPosition, float distance,string layer);
}