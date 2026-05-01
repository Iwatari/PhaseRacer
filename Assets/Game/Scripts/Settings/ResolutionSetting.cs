using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ResolutionSetting : Setting
{
    [SerializeField]
    private Vector2Int[] availibleResolutions = new Vector2Int[]
    {
        new Vector2Int(800,600),
        new Vector2Int(1280,720),
        new Vector2Int(1600,900),
        new Vector2Int(1920,1080),
    };

    private int currentResolutionIndex = 0;

    public override bool isMinValue { get => currentResolutionIndex == 0; }
    public override bool isMaxValue { get => currentResolutionIndex == availibleResolutions.Length -1; }

    public override void SetNextValue()
    {
        if(isMaxValue == false)
        {
            currentResolutionIndex++;
        }
    }

    public override void SetPreviousValue()
    {
        if (isMinValue == false)
        {
            currentResolutionIndex--;
        }
    }

    public override object GetValue()
    {
        return availibleResolutions[currentResolutionIndex];
    }

    public override string GetStringValue()
    {
        return availibleResolutions[currentResolutionIndex].x + "x" + availibleResolutions[currentResolutionIndex].y;
    }

    public override void Apply()
    {
        Screen.SetResolution(availibleResolutions[currentResolutionIndex].x, availibleResolutions[currentResolutionIndex].y, true);

        Save();
    }

    public override void Load()
    {
        currentResolutionIndex = PlayerPrefs.GetInt(title, availibleResolutions.Length - 1);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(title, currentResolutionIndex);
    }
}
