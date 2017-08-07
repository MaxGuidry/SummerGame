using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Launch : MonoBehaviour
{
    public Slider launchSlider;
    public Canvas launchCanvas;
    public Canvas angleCanvas;
    public Slider angleSlider;
    bool increasing;
    // Use this for initialization
    void Start()
    {
        angleCanvas.enabled = false;
        launchCanvas.enabled = false;

        increasing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (launchCanvas.enabled == true)
            SliderMove(launchSlider);
        else if (angleCanvas.enabled == true)
            SliderMove(angleSlider);
    }
    void SliderMove(Slider slider)
    {
        if (slider.value >= 100f)
        {
            increasing = false;
        }
        else if (slider.value <= 0f)
        {
            increasing = true;
        }
        if (increasing)
            slider.value += 1f;
        else
            slider.value -= 1f;

    }
}
