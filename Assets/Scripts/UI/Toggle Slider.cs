using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleSlider : MonoBehaviour
{
    public bool state = true;

    //objects
    public GameObject sliderText;
    public GameObject sliderBackground;
    public GameObject sliderCircle;

    public void SliderHit()
    {
        state = !state;
        SetState(state);

    }
    public void SetState(bool _state)
    {
        state=_state;
        float y = sliderCircle.transform.localPosition.y;
        if (state)
        {
            sliderText.GetComponent<TMP_Text>().text = "Attack Roll";
            sliderBackground.GetComponent<Image>().color = Color.green;
            sliderCircle.transform.localPosition = new Vector3(75, y, 0);
        }
        else
        {
            sliderText.GetComponent<TMP_Text>().text = "Standard Roll";
            sliderBackground.GetComponent<Image>().color = Color.grey;
            sliderCircle.transform.localPosition = new Vector3(-75, y, 0);
        }
    }


}
