using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleSlider : MonoBehaviour
{
    public bool active = true;

    //objects
    public GameObject sliderText;
    public GameObject sliderBackground;
    public GameObject sliderCircle;

    void Start()
    {
        
    }

    public void SliderHit()
    {
        active = !active;
        if (active)
        {
            sliderText.GetComponent<TMP_Text>().text = "Attack Roll";
            sliderBackground.GetComponent<Image>().color = Color.green;
            sliderCircle.transform.localPosition = new Vector3(75, -10, 0);
        }
        else
        {
            sliderText.GetComponent<TMP_Text>().text = "Standard Roll";
            sliderBackground.GetComponent<Image>().color = Color.grey;
            sliderCircle.transform.localPosition = new Vector3 (-75,-10,0);
        }

    }


}
