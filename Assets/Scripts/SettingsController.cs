using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SettingsController : MonoBehaviour
{
    public List<JumpSetting> JumpSettings;
    public Transform JumpPlace;

    public JumpSetting CurrentJumpSetting { get; private set; }

    [Header("Game Settings")]
    public float JumpPlaceScaleDuration = 1.0f;

    private void Start()
    {
        CurrentJumpSetting = JumpSettings[0];
    }

    public void SetNextJumpSetting()
    {
        CurrentJumpSetting = JumpSettings[(JumpSettings.IndexOf(CurrentJumpSetting) + 1) % JumpSettings.Count];
        var newJumpPlaceScale = new Vector3(CurrentJumpSetting.JumpPlaceSize, JumpPlace.localScale.y , CurrentJumpSetting.JumpPlaceSize);
        JumpPlace.DOScale(newJumpPlaceScale, JumpPlaceScaleDuration);
    }

    public void SetJumpSetting(int jumpSettingNumber)
    {
        CurrentJumpSetting = JumpSettings[jumpSettingNumber];
        JumpPlace.localScale = new Vector3(CurrentJumpSetting.JumpPlaceSize, JumpPlace.localScale.y, CurrentJumpSetting.JumpPlaceSize);
    }

    public int GetSettingNumber()
    {
        return JumpSettings.IndexOf(CurrentJumpSetting);
    }
}
