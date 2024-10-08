/*-------------------------------------------------
* PlayerJoinManager.cs
* 
* 作成日　2024/06/18
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの入退室の管理クラス
/// </summary>
public class PlayerJoinManager : MonoBehaviour
{
    // プレイヤーがゲームにJoinするためのInputAction
    [SerializeField] 
    private InputAction _playerJoinInputAction = default;

    // PlayerInputがアタッチされているプレイヤーオブジェクト
    [SerializeField] 
    private PlayerInput _playerPrefab = default;

    // 最大参加人数
    [SerializeField] 
    private int _maxPlayerCount = default;

    // Join済みのデバイス情報
    private InputDevice[] _joinedDevices = default;

    // 現在のプレイヤー数
    private int _currentPlayerCount = 0;


    private void Awake()
    {
        // 最大参加可能数で配列を初期化
        _joinedDevices = new InputDevice[_maxPlayerCount];

        // InputActionを有効化し、コールバックを設定
        _playerJoinInputAction.Enable();
        _playerJoinInputAction.performed += OnJoin;
    }

    private void OnDestroy()
    {
        _playerJoinInputAction.Dispose();
    }

    /// <summary>
    /// デバイスによってJoin要求が発火したときに呼ばれる処理
    /// </summary>
    private void OnJoin(InputAction.CallbackContext context)
    {
        // プレイヤー数が最大数に達していたら、処理を終了
        if (_currentPlayerCount >= _maxPlayerCount)
        {
            return;
        }

        // Join要求元のデバイスが既に参加済みのとき、処理を終了
        foreach (var device in _joinedDevices)
        {
            if (context.control.device == device)
            {
                return;
            }
        }

        // PlayerInputを所持した仮想のプレイヤーをインスタンス化
        // ※Join要求元のデバイス情報を紐づけてインスタンスを生成する
       
        PlayerInput.Instantiate(
            prefab: _playerPrefab.gameObject,
            playerIndex: _currentPlayerCount,
            pairWithDevice: context.control.device
            );

        // Joinしたデバイス情報を保存
        _joinedDevices[_currentPlayerCount] = context.control.device;
        _currentPlayerCount++;
    }
}
