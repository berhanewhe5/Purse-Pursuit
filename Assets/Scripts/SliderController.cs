using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderController : MonoBehaviour
{
    public bool sliderOn;
    public float sliderSpeed;
    public bool canSliderMove = true;
    public Slider pickPocketSlider;
    public bool isSliderMovingUp;

    void Start()
    {
        pickPocketSlider = GetComponent<Slider>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (canSliderMove)
        {
            if (isSliderMovingUp)
            {
                pickPocketSlider.value += sliderSpeed * Time.deltaTime;
                if (pickPocketSlider.value == 1)
                {
                    isSliderMovingUp = false;
                }
            }
            else
            {
                pickPocketSlider.value += -sliderSpeed * Time.deltaTime;
                if (pickPocketSlider.value == 0)
                {
                    isSliderMovingUp = true;
                }
            }
        }

    }

   


}
