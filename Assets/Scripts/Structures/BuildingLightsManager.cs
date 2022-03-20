using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLightsManager : MonoBehaviour
{
    public void SwitchOnLights()
    {
        SetActiveToChildren(true);
    }

    public void SwitchOffLights()
    {
        SetActiveToChildren(false);
    }

    private void SetActiveToChildren(bool enabled)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(enabled);
    }
}
