using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField] GameObject outlinePanel;
    private bool isSelecting;
    private bool isRising;
    private float alphaValue;
    private float speed = 1f;
    Image outlineImage;
    void Start()
    {
        isSelecting = false;
        isRising = false;
        alphaValue = 1f;
        outlineImage = outlinePanel.GetComponent<Image>();
    }
    void Update()
    {
        if (alphaValue >= 1f)
        {
            isRising = false;
            alphaValue -= speed * Time.deltaTime;
        }
        if (!isRising)
        {
            outlineImage.color = new Color(outlineImage.color.r, outlineImage.color.g, outlineImage.color.b, alphaValue);
            alphaValue -= speed * Time.deltaTime;
        }
        if (alphaValue <= 0f) 
        {
            isRising = true;
            alphaValue += speed * Time.deltaTime;
        }
        if (isRising)
        {
            outlineImage.color = new Color(outlineImage.color.r, outlineImage.color.g, outlineImage.color.b, alphaValue);
            alphaValue += speed * Time.deltaTime;
        }
    }
}
