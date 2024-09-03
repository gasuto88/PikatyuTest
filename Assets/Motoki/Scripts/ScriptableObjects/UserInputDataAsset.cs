/*-------------------------------------------------
* UserInputDataAsset.cs
* 
* 作成日　2024/06/21
*
* 作成者　本木大地
-------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ユーザー入力データクラス
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UserInputData")]
public class UserInputDataAsset : ScriptableObject
{
	#region フィールド変数

	[SerializeField,Header("入力デッドゾーン"),Min(0f)]
	private float _inputDeadZoon = 0.3f;

    private string _leftStickName = "LeftStick";

    private string _rightStickName = "RightStick"; 

    private string _leftTriggerName = "LeftTrigger";

    private string _rightTriggerName = "RightTrigger"; 

    private string _leftButtonName = "LeftButton";

    private string _rightButtonName = "RightButton";

    private string _buttonEastName = "ButtonEast";

    private string _buttonWestName = "ButtonWest";

    private string _buttonNorthName = "ButtonNorth";

    private string _buttonSouthName = "ButtonSouth";

    #endregion

    #region プロパティ

    public float InputDeadZoon { get => _inputDeadZoon; set => _inputDeadZoon = value; }

    public string LeftStickName { get => _leftStickName; set => _leftStickName = value; }

    public string RightStickName { get => _rightStickName; set => _rightStickName = value; }

    public string LeftTriggerName { get => _leftTriggerName; set => _leftTriggerName = value; }

    public string RightTriggerName { get => _rightTriggerName; set => _rightTriggerName = value; }

    public string LeftButtonName { get => _leftButtonName; set => _leftButtonName = value; }

    public string RightButtonName { get => _rightButtonName; set => _rightButtonName = value; }

    public string ButtonEastName { get => _buttonEastName; set => _buttonEastName = value; }

    public string ButtonWestName { get => _buttonWestName; set => _buttonWestName = value; }

    public string ButtonNorthName { get => _buttonNorthName; set => _buttonNorthName = value; }

    public string ButtonSouthName { get => _buttonSouthName; set => _buttonSouthName = value; }

    #endregion

}