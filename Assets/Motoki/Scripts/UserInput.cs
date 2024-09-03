/*-------------------------------------------------
* PlayerInput.cs
* 
* 作成日　2024/06/18
*
* 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

/// <summary>
/// ユーザー入力クラス
/// </summary>
public class UserInput : MonoBehaviour
{
    #region フィールド変数

    [SerializeField, Header("ユーザー入力データ")]
    private UserInputDataAsset _userInputDataAsset = default;

    private Vector2 _leftStickInput = default;
    private Vector2 _rightStickInput = default;

    private bool _isLeftTrigger = default;
    private bool _isRightTrigger = false;

    private bool _isLeftButton = false;
    private bool _isRightButton = false;

    private bool _isButtonEast = false;
    private bool _isButtonWest = false;
    private bool _isButtonNorth = false;
    private bool _isButtonSouth = false;

    private PlayerInput _playerInput = default;

    #endregion

    #region プロパティ

    public Vector2 LeftStickInput
    {
        get
        {
            // 入力デッドゾーン
            if (-_userInputDataAsset.InputDeadZoon <= _leftStickInput.x
                && _leftStickInput.x <= _userInputDataAsset.InputDeadZoon)
            {
                _leftStickInput.x = 0f;
            }
            if (-_userInputDataAsset.InputDeadZoon <= _leftStickInput.y
                && _leftStickInput.y <= _userInputDataAsset.InputDeadZoon)
            {
                _leftStickInput.y = 0f;
            }
            return _leftStickInput;
        }
    }

    public Vector2 RightStickInput
    {
        get
        {
            // 入力デッドゾーン
            if (-_userInputDataAsset.InputDeadZoon <= _rightStickInput.x
                && _rightStickInput.x <= _userInputDataAsset.InputDeadZoon)
            {
                _rightStickInput.x = 0f;
            }
            if (-_userInputDataAsset.InputDeadZoon <= _rightStickInput.y
                && _rightStickInput.y <= _userInputDataAsset.InputDeadZoon)
            {
                _rightStickInput.y = 0f;
            }
            return _rightStickInput;
        }
    }

    public bool IsLeftTrigger
    {
        get
        {
            return _isLeftTrigger;
        }
    }

    public bool IsRightTrigger
    {
        get
        {
            return _isRightTrigger;
        }
    }

    public bool IsLeftButton
    {
        get
        {
            return _isLeftButton;
        }
    }

    public bool IsRightButton
    {
        get
        {
            return _isRightButton;
        }
    }

    public bool IsButtonEast
    {
        get
        {
            return _isButtonEast;
        }
    }

    public bool IsButtonWest
    {
        get
        {
            return _isButtonWest;
        }
    }

    public bool IsButtonNorth
    {
        get
        {
            return _isButtonNorth;
        }
    }

    public bool IsButtonSouth
    {
        get
        {
            return _isButtonSouth;
        }
    }

    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        // Script取得
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.actions[_userInputDataAsset.LeftStickName].performed += OnLeftStick;
        _playerInput.actions[_userInputDataAsset.LeftStickName].canceled += OnLeftStick;

        _playerInput.actions[_userInputDataAsset.RightStickName].performed += OnRightStick;
        _playerInput.actions[_userInputDataAsset.RightStickName].canceled += OnRightStick;

        _playerInput.actions[_userInputDataAsset.LeftTriggerName].started += OnLeftTriggerDown;
        _playerInput.actions[_userInputDataAsset.LeftTriggerName].canceled += OnLeftTriggerUp;

        _playerInput.actions[_userInputDataAsset.RightTriggerName].started += OnRightTriggerDown;
        _playerInput.actions[_userInputDataAsset.RightTriggerName].canceled += OnRightTriggerUp;

        _playerInput.actions[_userInputDataAsset.LeftButtonName].started += OnLeftButtonDown;
        _playerInput.actions[_userInputDataAsset.LeftButtonName].canceled += OnLeftButtonUp;

        _playerInput.actions[_userInputDataAsset.RightButtonName].started += OnRightButtonDown;
        _playerInput.actions[_userInputDataAsset.RightButtonName].canceled += OnRightButtonUp;

        _playerInput.actions[_userInputDataAsset.ButtonEastName].started += OnButtonEastDown;
        _playerInput.actions[_userInputDataAsset.ButtonEastName].canceled += OnButtonEastUp;

        _playerInput.actions[_userInputDataAsset.ButtonWestName].started += OnButtonWestDown;
        _playerInput.actions[_userInputDataAsset.ButtonWestName].canceled += OnButtonWestUp;

        _playerInput.actions[_userInputDataAsset.ButtonNorthName].started += OnButtonNorthDown;
        _playerInput.actions[_userInputDataAsset.ButtonNorthName].canceled += OnButtonNorthUp;

        _playerInput.actions[_userInputDataAsset.ButtonSouthName].started += OnButtonSouthDown;
        _playerInput.actions[_userInputDataAsset.ButtonSouthName].canceled += OnButtonSouthUp;
    }


    private void OnLeftStick(InputAction.CallbackContext context)
    {
        _leftStickInput = context.ReadValue<Vector2>();
    }

    private void OnRightStick(InputAction.CallbackContext context)
    {
        _rightStickInput = context.ReadValue<Vector2>();
    }

    public void OnLeftTriggerDown(InputAction.CallbackContext context)
    {
        _isLeftTrigger = true;
    }

    public void OnLeftTriggerUp(InputAction.CallbackContext context)
    {
        _isLeftTrigger = false;
    }

    private void OnRightTriggerDown(InputAction.CallbackContext context)
    {
        _isRightTrigger = true;
    }

    private void OnRightTriggerUp(InputAction.CallbackContext context)
    {
        _isRightTrigger = false;
    }

    public void OnLeftButtonDown(InputAction.CallbackContext context)
    {
        _isLeftButton = true;
    }

    public void OnLeftButtonUp(InputAction.CallbackContext context)
    {
        _isLeftButton = false;
    }

    public void OnRightButtonDown(InputAction.CallbackContext context)
    {
        _isRightButton = true;
    }

    public void OnRightButtonUp(InputAction.CallbackContext context)
    {
        _isRightButton = false;
    }

    private void OnButtonEastDown(InputAction.CallbackContext context)
    {
        _isButtonEast = true;
    }

    private void OnButtonEastUp(InputAction.CallbackContext context)
    {
        _isButtonEast = false;
    }

    private void OnButtonWestDown(InputAction.CallbackContext context)
    {
        _isButtonWest = true;
    }

    private void OnButtonWestUp(InputAction.CallbackContext context)
    {
        _isButtonWest = false;
    }

    private void OnButtonNorthDown(InputAction.CallbackContext context)
    {
        _isButtonNorth = true;
    }

    private void OnButtonNorthUp(InputAction.CallbackContext context)
    {
        _isButtonNorth = false;
    }

    private void OnButtonSouthDown(InputAction.CallbackContext context)
    {
        _isButtonSouth = true;
    }

    private void OnButtonSouthUp(InputAction.CallbackContext context)
    {
        _isButtonSouth = false;
    }
}