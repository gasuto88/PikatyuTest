/*-------------------------------------------------
* Pikatyu.cs
* 
* 作成日　2024/06/25
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikatyu : Attack
{
    #region フィールド変数

	#endregion	

    public override void NormalAttack()
    {
        BallMove tempBall = default;

        switch (_normalAttackState)
        {
            case NormalAttackState.START:
                {
                    tempBall = _ballPool.TakeOut(_player.PlayerPosition, _player.PlayerRotation);

                    _normalAttackState = NormalAttackState.ATTACK;

                    break;
                }              
            case NormalAttackState.ATTACK:
                {
                    //tempBall.MoveBall();

                    break;
                }             
            case NormalAttackState.END:
                break;
            default:
                break;
        }

        
    }

    public override void RoleAttack()
    {
        
    }
}