using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetsMenu : MonoBehaviour
{
    public GameObject attackPresetPanelPrefab;
    public GameObject damagePresetPanelPrefab;

    public void Init()
    {
        Presets.LoadFromJSON();
        foreach(DieGroup dieGroup in Presets.dieGroups)
        {

        }
    }

    public void LoadBtn()
    {

    }
    public void BackBtn()
    {

    }
}
