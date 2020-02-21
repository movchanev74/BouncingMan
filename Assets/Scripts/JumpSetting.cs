using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpSettingData", menuName = "Settings/JumpSetting")]
public class JumpSetting : ScriptableObject
{
    public float JumpHeight = 2f;
    public float JumpPlaceSize = 1;
}
