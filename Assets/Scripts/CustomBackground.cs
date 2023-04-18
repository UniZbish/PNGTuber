using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomBackground : MonoBehaviour
{
    [SerializeField] private Camera backgroundCol;
    [SerializeField] private GameObject backgroundMenu;

    [SerializeField] private Slider hueSlider, satSlider, brightSlider;
    [SerializeField] private Image hueSliderImage, satSliderImage, brightSliderImage;

    [SerializeField] private InputField hexInput;

    private Texture2D hueTexture, satTexture, brightValTexture;

    private Vector3 colourHSV;

    void Start()
    {
        CreateHueTexture();
        CreateSVTextures();
        UpdateBackground();
        ToggleVisabillity(true);
    }

    private void CreateHueTexture()
    {
        hueTexture = new Texture2D(360, 1);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        for (int x = 0; x < 360; x++)
        {
            hueTexture.SetPixel(x, 1, Color.HSVToRGB(((float)x / 360), 1, 1));
        }
        hueTexture.Apply();
        hueSliderImage.sprite = Sprite.Create(hueTexture, new Rect(0.0f, 0.0f, hueTexture.width, hueTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    public void CreateSVTextures()
    {
        if (satTexture != null)
        {
            DestroyImmediate(satTexture);
        }

        satTexture = new Texture2D(255, 1);
        satTexture.wrapMode = TextureWrapMode.Clamp;
        for (int x = 0; x < 255; x++)
        {
            satTexture.SetPixel(x, 1, Color.HSVToRGB(hueSlider.value, ((float)x / 255), brightSlider.value));
        }
        satTexture.Apply();
        satSliderImage.sprite = Sprite.Create(satTexture, new Rect(0.0f, 0.0f, satTexture.width, satTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

        if (brightValTexture != null)
        {
            DestroyImmediate(brightValTexture);
        }

        brightValTexture = new Texture2D(255, 1);
        brightValTexture.wrapMode = TextureWrapMode.Clamp;
        for (int x = 0; x < 255; x++)
        {
            brightValTexture.SetPixel(x, 1, Color.HSVToRGB(hueSlider.value, satSlider.value, ((float)x / 255)));
        }
        brightValTexture.Apply();
        brightSliderImage.sprite = Sprite.Create(brightValTexture, new Rect(0.0f, 0.0f, brightValTexture.width, brightValTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    public void HexToRGB()
    {
        Color hexColor = new Color();
        ColorUtility.TryParseHtmlString(hexInput.text, out hexColor);

        float H, S, V;
        Color.RGBToHSV(hexColor, out H, out S, out V);
        hueSlider.value = H;
        satSlider.value = S;
        brightSlider.value = V;
        UpdateBackground();
    }

    public void ToggleVisabillity(bool close) 
    {
        if (!close)
        {
            backgroundMenu.SetActive(false);
        }
        else 
        {
            backgroundMenu.SetActive(!backgroundMenu.activeSelf);
        }
    }
    public void UpdateBackground()
    {
        colourHSV.x = hueSlider.value;
        colourHSV.y = satSlider.value;
        colourHSV.z = brightSlider.value;
        backgroundCol.backgroundColor = Color.HSVToRGB(colourHSV.x, colourHSV.y, colourHSV.z);
        string hexCode = ColorUtility.ToHtmlStringRGB(backgroundCol.backgroundColor);
        hexInput.text = "#" + hexCode;
    }
}
