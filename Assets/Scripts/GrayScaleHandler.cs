using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrayScaleHandler : MonoBehaviour
{
    public float scale = 1.0f;
    public OxygenHandler OxygenHandler;

    private Image grayScaleImage;
    // Start is called before the first frame update
    void Start()
    {
        OxygenHandler = GameObject.FindObjectOfType<OxygenHandler>();
        grayScaleImage = GameObject.Find("GrayScalePanel").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        scale =  1.0f - (OxygenHandler.OxygenLeft / 100.0f);
        var currentColor = grayScaleImage.color;
        currentColor.a = scale;
        currentColor.r = currentColor.r * scale;
        currentColor.g = currentColor.g * scale;
        currentColor.b = currentColor.b * scale;
        grayScaleImage.color = currentColor;
    }
}
