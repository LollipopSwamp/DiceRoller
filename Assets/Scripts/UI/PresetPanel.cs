using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetPanel : MonoBehaviour
{
    public int index;
    public DieGroup diegroup;

    //panels
    public List<GameObject> standardTextPanels;
    public List<GameObject> attackTextPanels;

    public void Init(int _index, DieGroup _dieGroup)
    {
        index = _index;
    }
}
