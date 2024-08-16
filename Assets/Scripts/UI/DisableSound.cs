using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSound : MonoBehaviour
{
    public bool disableSound = false;
    public GameObject check;

    public void Init(bool _disableSound)
    {
        disableSound = _disableSound;
        check.SetActive(disableSound);
    }

    public void ButtonHit()
    {
        disableSound = !disableSound;
        check.SetActive(disableSound);
        Settings.SetDisableSound(disableSound);
    }
}
