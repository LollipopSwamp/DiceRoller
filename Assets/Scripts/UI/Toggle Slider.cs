using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSlider : MonoBehaviour
{
    public bool active = true;

    //objects
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
            sliderBackground.GetComponent<Image>().color = Color.green;
            sliderCircle.transform.localPosition = new Vector3(25, -10, 0);
        }
        else
        {
            sliderBackground.GetComponent<Image>().color = Color.grey;
            sliderCircle.transform.localPosition = new Vector3 (-25,-10,0);
        }

    }


}
